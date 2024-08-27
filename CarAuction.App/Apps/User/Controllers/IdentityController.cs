using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Identity;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class IdentityController(IIdentityService identityService) : ControllerBase
    {
        [HttpPost]
        [ActionName("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var response = await identityService.Register(dto, "User");
            return StatusCode(response.StatusCode, response);

        }

        [HttpPut]
        [ActionName("Update")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<IActionResult> Update(UpdateDto dto)
        {
            var response = await identityService.UpdateUser(dto);
            return StatusCode(response.StatusCode, response);

        }

        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response = await identityService.Login(dto, 30,"User");
            return StatusCode(response.StatusCode, response);

        }

        [HttpPost]
        [ActionName("Google-Login")]
        public async Task<IActionResult> Google_Login(string IdToken)
        {
            var response = await identityService.GoogleLogin(IdToken, 900);
            return StatusCode(response.StatusCode, response);

        }
    }
}
