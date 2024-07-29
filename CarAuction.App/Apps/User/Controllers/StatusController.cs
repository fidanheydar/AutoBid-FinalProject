using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Users.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _StatusService;

        public StatusController(IStatusService StatusService)
        {
            _StatusService = StatusService;
        }
        [HttpGet]
        [ActionName("GetStatuses")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await _StatusService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        [ActionName("GetStatus")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await _StatusService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
