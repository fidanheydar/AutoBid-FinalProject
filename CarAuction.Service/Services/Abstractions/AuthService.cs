using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Auth;
using CarAuction.Service.DTOs.Mail;
using CarAuction.Service.Exceptions;
using CarAuction.Service.Extensions;
using CarAuction.Service.Responses;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CarAuction.Service.Services.Abstractions
{
    public class AuthService : IAuthService
    {
        readonly UserManager<AppUser> _userManager;
        readonly ITokenService _tokenService;
        readonly IMailService _mailService;

        public AuthService(UserManager<AppUser> userManager, ITokenService tokenService, IMailService mailService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mailService = mailService;
        }

        public async Task<ApiResponse> ResetPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            ResetTokenEmailResponseDTO emailResponseDto = new();
            if (user is not null)
            {
                var resetTokenResponse = await _userManager.GeneratePasswordResetTokenAsync(user);
                var newResetToken = resetTokenResponse.EncodeToken();
                emailResponseDto.Email = email;
                emailResponseDto.Token = newResetToken;
                emailResponseDto.UserId = user.Id;
                string resetLink = $"https://localhost:7105/resetPassword?token={newResetToken}&userId={user.Id}";

                await _mailService.SendEmailAsync(new MailRequestDTO
                {
                    Attachments = null,
                    Subject = "ResetPassword",
                    Body = $"Please click on the link below to reset your password: \n {resetLink}",
                    ToEmails = new List<string> { user.Email },
                });
                ;
                return new()
                {
                    items = emailResponseDto,
                    StatusCode = (int)HttpStatusCode.OK,
                    Description = " Reset Token Generated"
                };
            }

            return new()
            {
                items = null,
                StatusCode = (int)HttpStatusCode.NotFound,
                Description = " Reset not generated"
            };
        }

        public async Task<ApiResponse> VerifyPasswordResetToken(ResetResponseDTO resetResponseDto)
        {
            var user = await _userManager.FindByIdAsync(resetResponseDto.UserId);
            bool isValidToken = false;
            if (user is not null)
            {
                var decodedResetToken = resetResponseDto.ResetToken.DecodeToken();
                isValidToken = await _userManager.VerifyUserTokenAsync(user,
                    _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", decodedResetToken);
            }

            return new()
            {
                StatusCode = (int)(isValidToken ? HttpStatusCode.OK : HttpStatusCode.BadRequest),
                Description = $"{isValidToken}"
            };
        }

        public async Task<ApiResponse> ConfirmEmail(ConfirmEmailDTO dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (dto?.UserId == null || dto?.Token == null)
            {
                return new ApiResponse()
                {
                    StatusCode = 410,
                    Description = "Link expired"
                };
            }
            else if (user == null)
            {
                return new ApiResponse()
                {
                    StatusCode = 404,
                    Description = "User not Found"
                };
            }
            else
            {
                dto.Token = dto.Token.Replace(" ", "+");
                var result = await _userManager.ConfirmEmailAsync(user, dto.Token);
                if (result.Succeeded)
                {
                    return new ApiResponse()
                    {
                        StatusCode = 200,
                        Description = "Thank you for confirming your email"
                    };
                }
                else
                {
                    return new ApiResponse()
                    {
                        StatusCode = 400,
                        Description = "Email not confirmed"
                    };
                }
            }
        }

        public async Task<ApiResponse> UpdatePasswordAsync(UpdatePasswordDTO updatePasswordDto)
        {
            var user = await _userManager.FindByIdAsync(updatePasswordDto.UserId);
            bool isUpdated = false;
            if (user is not null)
            {
                var decodedResetToken = updatePasswordDto.ResetToken.DecodeToken();
                IdentityResult resetPassword =
                    await _userManager.ResetPasswordAsync(user, decodedResetToken, updatePasswordDto.NewPassword);
                if (resetPassword.Succeeded)
                {
                    IdentityResult result = await _userManager.UpdateSecurityStampAsync(user);
                    isUpdated = result.Succeeded;
                }
            }

            return new ApiResponse()
            {
                StatusCode = (int)(isUpdated ? HttpStatusCode.OK : HttpStatusCode.BadRequest),
            };
        }

        public async Task UpdateRefreshToken(AppUser user, string refreshToken, DateTime accessTokenLifeTime,
            int addMinuteToLifeTime)
        {
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpirationDate = accessTokenLifeTime.AddMinutes(addMinuteToLifeTime);
            await _userManager.UpdateAsync(user);
        }

        public async Task<ApiResponse> RefreshTokenLoginAsync(string refreshToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user is not null && user.RefreshTokenExpirationDate > DateTime.UtcNow)
            {
                TokenResponseDTO tokenResponse = await _tokenService.CreateAccessTokenAsync(user, 10);
                await UpdateRefreshToken(user, tokenResponse.RefreshToken, tokenResponse.Expiration, 5);
                return new()
                {
                    items = tokenResponse,
                    StatusCode = 200
                };
            }

            throw new TokenFailedException();
        }
    }
}