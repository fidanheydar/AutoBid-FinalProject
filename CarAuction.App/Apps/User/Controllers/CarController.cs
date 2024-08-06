using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class CarController(ICarService carService) : ControllerBase
    {
        [HttpGet]
        [ActionName("GetShopCars")]
        public async Task<IActionResult> GetShopCars([FromQuery] int page, int count)
        {
            var response = await carService.GetAllAsync(count, page,x=>x.Status.Level!=3);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [ActionName("GetArchiveCars")]
        public async Task<IActionResult> GetArchiveCars([FromQuery] int page, int count)
        {
            var response = await carService.GetAllAsync(count, page, x => x.Status.Level == 3);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [ActionName("GetCar")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await carService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [ActionName("AdvancedFilter")]
        public async Task<IActionResult> AdvancedFilter([FromQuery] string? brand, Guid? model, double? minprice, double? maxprice, int? minyear, int? maxyear, Guid? color, Guid? ban, Guid? fuel, int page = 1)
        {
            var response = await carService.AdvancedSearch(new()
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
