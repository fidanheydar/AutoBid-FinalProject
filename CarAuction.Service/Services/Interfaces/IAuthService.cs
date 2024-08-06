using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Auth;
using CarAuction.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse> UpdatePasswordAsync(UpdatePasswordDTO updatePasswordDto);
        Task UpdateRefreshToken(AppUser user, string RefreshToken, DateTime accessTokenLifeTime, int addMinuteToLifeTime);
        Task<ApiResponse> RefreshTokenLoginAsync(string refreshToken);
        Task<ApiResponse> ResetPassword(string email);
        Task<ApiResponse> VerifyPasswordResetToken(ResetResponseDTO resetResponseDto);
        Task<ApiResponse> ConfirmEmail(ConfirmEmailDTO confirmEmailDto);
    }
}
