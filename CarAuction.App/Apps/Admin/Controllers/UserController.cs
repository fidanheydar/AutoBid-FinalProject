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
    [Authorize(Roles ="Admin")]
    public class UserController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public UserController(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers([FromQuery]int count,int page)
        {
            var response = await _identityService.GetAllUsers(count,page,"User");
            return StatusCode(response.StatusCode, response);
        }


        [HttpDelete("{id}")]
        [ActionName("DeleteUser")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _identityService.Remove(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
