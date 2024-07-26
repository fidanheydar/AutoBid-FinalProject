using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Users.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class FuelController : ControllerBase
    {
        private readonly IFuelService _fuelService;

        public FuelController(IFuelService fuelService)
        {
            _fuelService = fuelService;
        }
        [HttpGet]
        [ActionName("GetFuels")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await _fuelService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        [ActionName("GetFuel")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await _fuelService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
