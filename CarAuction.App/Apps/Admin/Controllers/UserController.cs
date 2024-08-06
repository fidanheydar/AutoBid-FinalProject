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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class UserController(IIdentityService identityService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int count,int page)
        {
            var response = await identityService.GetAllUsers(count,page,"User");
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
