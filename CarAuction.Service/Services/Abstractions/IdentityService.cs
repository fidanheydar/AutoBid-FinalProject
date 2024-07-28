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
using static System.Net.WebRequestMethods;

namespace CarAuction.Service.Services.Abstractions
{
    internal class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _contextAccessor;

        public IdentityService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IAuthService authService, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _authService = authService;
            _contextAccessor = contextAccessor;
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

            if (role != null)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (!roles.Contains("Admin"))
                {
                    throw new LoginFailedException();
                }
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
            return new()
            {
                items = tokenResponseDto,
                StatusCode = (int)HttpStatusCode.OK,
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


            AppUser? appUser = default;
            if (!string.IsNullOrWhiteSpace(userName))
            {
                appUser = await _userManager.FindByNameAsync(userName);

                appUser.Name = dto.Name;
                appUser.Surname = dto.Surname;
                appUser.Email = dto.Email;
                appUser.UserName = dto.UserName;

                await _userManager.UpdateAsync(appUser);
                if (!string.IsNullOrWhiteSpace(dto.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
                    var result = await _userManager.ResetPasswordAsync(appUser, token, dto.Password);
                    var loginResult = await Login(new LoginDto { EmailorUserName = dto.Email, Password = dto.Password }, 100);
                    return new()
                    {
                        items = loginResult,
                        StatusCode = 200,
                    };
                }
                await _signInManager.SignInAsync(appUser, false);
                return new()
                {
                    StatusCode = 200,
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
