using CarAuction.Service.DTOs.Auth;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [ActionName("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            var response = await _authService.ResetPassword(dto.Email);
            return Ok(response);
        }

        [HttpPost]
        [ActionName("VerifyPasswordResetToken")]
        public async Task<IActionResult> VerifyPasswordResetToken(ResetResponseDTO dto)
        {
            var response = await _authService.VerifyPasswordResetToken(dto);
            return Ok(response);
        }

        [HttpPost]
        [ActionName("UpdatePasswordAsync")]
        public async Task<IActionResult> UpdatePasswordAsync(UpdatePasswordDTO dto)
        {
            var response = await _authService.UpdatePasswordAsync(dto);
            return Ok(response);
        }

        [HttpPost]
        [ActionName("RefreshTokenLoginAsync")]
        public async Task<IActionResult> RefreshTokenLoginAsync(string refreshToken)
        {
            var response = await _authService.RefreshTokenLoginAsync(refreshToken);
            return Ok(response);
        }
    }
}
