using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Users.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BanController : ControllerBase
    {
        private readonly IBanService _banService;

        public BanController(IBanService banService)
        {
            _banService = banService;
        }
        [HttpGet]
        [ActionName("GetBans")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await _banService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        [ActionName("GetBan")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await _banService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
