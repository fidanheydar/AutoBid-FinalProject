using CarAuction.Service.DTOs.Bans;
using CarAuction.Service.DTOs.Identity;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "SuperAdmin")]
    public class AdminController(IIdentityService identityService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] RegisterDto dto)
        {
            var response = await identityService.Register(dto,"Admin");
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{userName}")]
        [ActionName("GetAdmin")]
        public async Task<IActionResult> Get([FromRoute]string userName)
        {
            var response = await identityService.GetUserByName(userName);
            if(response is null)
            {
                return NotFound();
            }
            return StatusCode(200, response);
        }

        [HttpGet]
        [ActionName("GetAdmins")]
        public async Task<IActionResult> Get([FromQuery] int count, int page)
        {
            var response = await identityService.GetAllUsers(count, page, "Admin");
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] UpdateDto dto)
        {
            var response = await identityService.UpdateUser(dto, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await identityService.Remove(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
