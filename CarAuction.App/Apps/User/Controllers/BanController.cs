using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BanController(IBanService banService) : ControllerBase
    {
        [HttpGet]
        [ActionName("GetBans")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await banService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [ActionName("GetBan")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await banService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
