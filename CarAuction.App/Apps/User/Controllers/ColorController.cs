using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ColorController(IColorService colorService) : ControllerBase
    {
        [HttpGet]
        [ActionName("GetColors")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await colorService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [ActionName("GetColor")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await colorService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
