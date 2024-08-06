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
    public class SubscribeController(ISubscribeService subscribeService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int page,int count)
        {
            var response = await subscribeService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await subscribeService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
