using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Auth;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Abstractions
{
    public class TokenService : ITokenService
    {
        readonly UserManager<AppUser> _userManager;
        readonly IConfiguration _configuration;

        public TokenService(UserManager<AppUser> userManager, IConfiguration configuratixon)
        {
            _userManager = userManager;
            _configuration = configuratixon;
        }

        public async Task<TokenResponseDTO> CreateAccessTokenAsync(AppUser user, int minute)
        {
            TokenResponseDTO tokenResponseDto = new();
            tokenResponseDto.Expiration = DateTime.Now.AddMinutes(minute);
            List<Claim> myClaims = new() {
               new(ClaimTypes.Name,user.UserName),
               new(ClaimTypes.Email,user.Email),
               new(ClaimTypes.NameIdentifier,user.Id)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                myClaims.Add(new(ClaimTypes.Role, role));
            }

            SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtTokenSettings:SignInKey"]));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: _configuration["JwtTokenSettings:Issuer"],
                audience: _configuration["JwtTokenSettings:Audience"],
                claims: myClaims,
                notBefore: DateTime.Now,
                expires: tokenResponseDto.Expiration,
                signingCredentials: signingCredentials
                );


            JwtSecurityTokenHandler tokenHandler = new();
            tokenResponseDto.AccessToken = tokenHandler.WriteToken(jwtSecurityToken);
            tokenResponseDto.RefreshToken = CreateRefreshToken();
            return tokenResponseDto;
        }

        public string CreateRefreshToken()
        {
            byte[] bytes = new byte[64];
            using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
            randomNumberGenerator.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}
