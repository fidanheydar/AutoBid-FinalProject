using CarAuction.Service.DTOs.Identity;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class IdentityController(IIdentityService identityService) : ControllerBase
    {
        [HttpPost]
        [ActionName("AdminLogin")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var response = await identityService.Login(dto, 400, "Admin");
            return Ok(response);
        }
    }
}
