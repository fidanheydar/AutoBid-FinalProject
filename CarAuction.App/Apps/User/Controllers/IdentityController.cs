using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Identity;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        readonly IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpPost]
        [ActionName("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var response = await _identityService.Register(dto, "User");
            return StatusCode(response.StatusCode, response);

        }

        [HttpPut]
        [ActionName("Update")]
        public async Task<IActionResult> Update(UpdateDto dto)
        {
            var response = await _identityService.UpdateUser(dto);
            return StatusCode(response.StatusCode, response);

        }
        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response = await _identityService.Login(dto, 30);
            return StatusCode(response.StatusCode, response);

        }
        //[HttpPost]
        //[ActionName("AddRole")]
        //public async Task<IActionResult> AddRole()
        //{
        //    AppRole role1 = new()
        //    {
        //        Name = "User"
        //    };
        //    AppRole role2 = new()
        //    {
        //        Name = "Admin"
        //    };
        //    AppRole role3 = new()
        //    {
        //        Name = "SuperAdmin"
        //    };
        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);
        //    return Ok();
        //}
    }
}
