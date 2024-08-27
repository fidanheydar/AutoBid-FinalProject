using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Auth;
using CarAuction.Service.DTOs.Identity;
using CarAuction.Service.Exceptions;
using CarAuction.Service.Responses;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using static System.Net.WebRequestMethods;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;

namespace CarAuction.Service.Services.Abstractions
{
    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;

        public IdentityService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IAuthService authService, IHttpContextAccessor contextAccessor, IMailService mailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _authService = authService;
            _contextAccessor = contextAccessor;
            _mailService = mailService;
            _configuration = configuration;
        }
        public async Task<ApiResponse> Register(RegisterDto registerDto, string role = null)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user is not null)
            {
                throw new ItemAlreadyExistsException($"User already exists");
            }

            user = new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                Name = registerDto.Name,
                Surname = registerDto.Surname
            };

            if (role is not "User")
            {
                user.EmailConfirmed = true;
            }

            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                return new ApiResponse()
                {
                    StatusCode = 500,
                    items = result.Errors
                };
            }
            await _userManager.AddToRoleAsync(user, role);
            if (role is "User")
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var confirmationLink = $"https://localhost:7105/api/v1/Auth/ConfirmEmail?UserId={user.Id}&Token={token}";
                await _mailService.SendEmailAsync(new()
                {
                    ToEmails =  new List<string>(){user.Email},
                    Attachments = null,
                    Subject = "Confirm Your Email" ,
                    Body = $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>:."
                });
            }
            
            return new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Description = "User Succesfully Registered"
            };
        }
        public async Task<ApiResponse> Login(LoginDto loginDto, int accessTokenLifeTime, string role = null)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.EmailorUserName);
            if (user is null)
            {
                user = await _userManager.FindByNameAsync(loginDto.EmailorUserName);
                if (user is null)
                    throw new LoginFailedException();
            }

            //if (role != null)
            //{
            //    var roles = await _userManager.GetRolesAsync(user);
            //    if (!roles.Contains("Admin"))
            //    {
            //        throw new LoginFailedException();
            //    }
            //}

            if (role != null && !await _userManager.IsInRoleAsync(user, role))
            {
                throw new LoginFailedException();
            }


            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
            if (!result.Succeeded)
            {
                throw new LoginFailedException();
            }
            if (accessTokenLifeTime == 200)
            {
                await _signInManager.SignInAsync(user, false);
                return new()
                {
                    StatusCode = (int)HttpStatusCode.OK,
                };
            }
            TokenResponseDTO tokenResponseDto = await _tokenService.CreateAccessTokenAsync(user, accessTokenLifeTime);
            await _authService.UpdateRefreshToken(user, tokenResponseDto.RefreshToken, tokenResponseDto.Expiration, 5);
            return new ApiResponse
            {
                items = tokenResponseDto,
                StatusCode = (int)HttpStatusCode.OK,
            };
        }
        public async Task<ApiResponse> GoogleLogin(string idToken,int accessTokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            UserLoginInfo info = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");

            AppUser? user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);

            bool result = user != null;

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(payload.Email);
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                    };
                    var identityResult = await _userManager.CreateAsync(user);
                    result = identityResult.Succeeded;
                }

            }
            if (result)
                await _userManager.AddLoginAsync(user, info);
            else
                throw new Exception("Invalid external authentication");

            var token = await _tokenService.CreateAccessTokenAsync(user,accessTokenLifeTime);
            await _authService.UpdateRefreshToken(user,token.RefreshToken, token.Expiration, 300);

            return new ApiResponse()
            {
                StatusCode=200,
                items=token
            };
        }
        public async Task<ApiResponse> GetAllUsers(int count, int page, string role)
        {
            List<AppUser> users = new();

            var userList = await _userManager.Users.ToListAsync();

            foreach (var user in userList)
            {
                if (await _userManager.IsInRoleAsync(user, role))
                {
                    users.Add(user);
                }
            };

            var items = users.Select(c => new UserGetDto()
            {
                Id = c.Id,
                Name = c.Name,
                Surname = c.Surname,
                Email = c.Email,
                EmailConfirmed = c.EmailConfirmed,
                UserName = c.UserName
            });
            if (page != 0 && count != 0)
                items = items.Skip((page - 1) * count).Take(count);

            return new ApiResponse()
            {
                StatusCode = 200,
                items = items.ToList(),
            };
        }
        public async Task<ApiResponse> UpdateUser(UpdateDto dto, string id = null)
        {
            string userName = string.Empty;
            if (id != null)
                userName = (await _userManager.FindByIdAsync(id)).UserName;
            else
                userName = _contextAccessor.HttpContext?.User.Identity.Name;


            if (!string.IsNullOrWhiteSpace(userName))
            {
                AppUser? appUser = default;

                appUser = await _userManager.FindByNameAsync(userName);

                if (appUser == null)
                {
                    return new()
                    {
                        StatusCode = 404,
                    };
                }


                if (!string.IsNullOrWhiteSpace(dto.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                    var result = await _userManager.ResetPasswordAsync(appUser, token, dto.Password);
                    if (result.Succeeded)
                    {
                        var loginResult = await Login(new LoginDto { EmailorUserName = dto.Email, Password = dto.Password }, 200);
                        return new()
                        {
                            items = loginResult,
                            StatusCode = 200,
                        };
                    }
                    else
                    {
                        return new()
                        {
                            items = result.Errors,
                            StatusCode = 400,
                        };
                    }
                }

                appUser.Name = dto.Name;
                appUser.Surname = dto.Surname;
                appUser.Email = dto.Email;
                appUser.UserName = dto.UserName;

                await _userManager.UpdateAsync(appUser);
              
                await _signInManager.SignInAsync(appUser, false);
                return new()
                {
                    StatusCode = 204,
                };
            }

            return new() { StatusCode = 403 };
        }
        public async Task<AppUser> GetUserByName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }
        public async Task<ApiResponse> Remove(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return new()
                {
                    StatusCode = 404
                };
            }
            await _userManager.DeleteAsync(user);
            return new()
            {
                StatusCode = 203
            };
        }
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
