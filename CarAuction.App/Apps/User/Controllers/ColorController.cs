using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Users.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }
        [HttpGet]
        [ActionName("GetColors")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await _colorService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        [ActionName("GetColor")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await _colorService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
