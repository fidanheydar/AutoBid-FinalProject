using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
    public class SubscribeController(ISubscribeService subscribeService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            var response = await subscribeService.CreateAsync(email);
            return StatusCode(response.StatusCode, response);
        }
    }
}
