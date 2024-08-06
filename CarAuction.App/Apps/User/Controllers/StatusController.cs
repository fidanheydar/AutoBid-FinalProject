using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class StatusController(IStatusService statusService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await statusService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await statusService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
