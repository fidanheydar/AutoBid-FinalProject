using CarAuction.Service.DTOs.Identity;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
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
        //[Authorize(Roles = "Admin")]
        [ActionName("CreateAdmin")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var response = await _identityService.Register(dto, "Admin");
            return Ok(response);
        }
        [HttpPost]
        [ActionName("AdminLogin")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response = await _identityService.Login(dto, 400, "Admin");
            return Ok(response);
        }
    }
}
