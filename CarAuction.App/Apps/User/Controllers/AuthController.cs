using CarAuction.Service.DTOs.Auth;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost]
        [ActionName("ResetPassword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO dto)
        {
            var response = await authService.ResetPassword(dto.Email);
            return StatusCode(response.StatusCode,response);
        }

        [HttpPost]
        [ActionName("VerifyPasswordResetToken")]
        public async Task<IActionResult> VerifyPasswordResetToken(ResetResponseDTO dto)
        {
            var response = await authService.VerifyPasswordResetToken(dto);
            return StatusCode(response.StatusCode,response);
        }

        [HttpPost]
        [ActionName("UpdatePasswordAsync")]
        public async Task<IActionResult> UpdatePasswordAsync(UpdatePasswordDTO dto)
        {
            var response = await authService.UpdatePasswordAsync(dto);
            return StatusCode(response.StatusCode,response);
        }

        [HttpPost]
        [ActionName("RefreshTokenLoginAsync")]
        public async Task<IActionResult> RefreshTokenLoginAsync(string refreshToken)
        {
            var response = await authService.RefreshTokenLoginAsync(refreshToken);
            return StatusCode(response.StatusCode,response);
        }
        
        [HttpGet]
        [ActionName("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailDTO dto)
        {
            var response = await authService.ConfirmEmail(dto);
            return StatusCode(response.StatusCode,response);
        }
    }
}
