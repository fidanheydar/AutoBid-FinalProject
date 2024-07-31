using CarAuction.Service.DTOs.Bans;
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
    [Authorize(Roles ="SuperAdmin")]
    public class AdminController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AdminController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        [ActionName("CreateAdmin")]
        public async Task<IActionResult> Post([FromForm] RegisterDto dto)
        {
            var response = await _identityService.Register(dto,"Admin");
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [ActionName("GetAdmin")]
        public async Task<IActionResult> GetAdmin([FromRoute]string userName)
        {
            var response = await _identityService.GetUserByName(userName);
            return StatusCode(200, response);
        }

        [HttpGet]
        [ActionName("GetAllAdmins")]
        public async Task<IActionResult> GetAllAdmins([FromQuery] int count, int page)
        {
            var response = await _identityService.GetAllUsers(count, page, "Admin");
            return StatusCode(response.StatusCode, response);
        }

        [HttpPut("{id}")]
        [ActionName("UpdateAdmin")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] UpdateDto dto)
        {
            var response = await _identityService.UpdateUser(dto, id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpDelete("{id}")]
        [ActionName("DeleteAdmin")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _identityService.Remove(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
