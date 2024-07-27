using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Users.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _CarService;

        public CarController(ICarService CarService)
        {
            _CarService = CarService;
        }
        [HttpGet]
        [ActionName("GetCars")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await _CarService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        [ActionName("GetCar")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await _CarService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [ActionName("AdvancedSearch")]
        public async Task<IActionResult> AdvancedSearch([FromQuery] string? brand, Guid? model, double? minprice, double? maxprice, int? minyear, int? maxyear, Guid? color, Guid? ban, Guid? fuel, int page = 1)
        {
            var response = await _CarService.AdvancedSearch(new()
            {
                ban = ban,
                brand = brand,
                color = color,
                fuel = fuel,
                page = page,
                maxprice = maxprice,
                minprice = minprice,
                model = model,
                maxyear = maxyear,
                minyear = minyear,
            });
            return StatusCode(response.StatusCode, response);
        }

    }
}
