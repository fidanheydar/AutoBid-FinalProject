using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Auth;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class AuthController(IAuthService authService,UserManager<AppUser> _userManager,RoleManager<AppRole> _roleManager) : ControllerBase
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

        //[HttpPost]
        //[ActionName("AdminCreate")]
        //public async Task<IActionResult> AdminCreate()
        //{
        //    AppUser SuperAdmin = new AppUser
        //    {
        //        Name = "Fidan",
        //        Surname = "Haydarov",
        //        Email = "fidan@auction.com",
        //        UserName = "SuperAdmin",
        //        EmailConfirmed = true
        //    };
           
        //    await _userManager.CreateAsync(SuperAdmin, "Fidan123.");

        //    await _userManager.AddToRoleAsync(SuperAdmin, "SuperAdmin");
        //    await _userManager.AddToRoleAsync(SuperAdmin, "Admin");
        //    return Ok();
        //}
        //[HttpPost]
        //[ActionName("AddRoles")]
        //public async Task<IActionResult> AddRoles()
        //{
        //    AppRole role = new() 
        //    {
        //        Name = "User"
        //    };
        //    AppRole role1 = new()
        //    {
        //        Name = "Admin"
        //    };
        //    AppRole role2 = new()
        //    {
        //        Name = "SuperAdmin"
        //    };
        //    await _roleManager.CreateAsync(role);
        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    return Ok();
        //}
    }
}
