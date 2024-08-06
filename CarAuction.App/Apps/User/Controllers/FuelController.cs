using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class FuelController(IFuelService fuelService) : ControllerBase
    {
        [HttpGet]
        [ActionName("GetFuels")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await fuelService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [ActionName("GetFuel")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await fuelService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
