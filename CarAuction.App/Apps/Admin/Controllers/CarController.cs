using CarAuction.Service.DTOs.Cars;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _CarService;

        public CarController(ICarService CarService)
        {
            _CarService = CarService;
        }
        [HttpPost]
        [ActionName("CreateCar")]
        public async Task<IActionResult> Post([FromForm] CarPostDto dto)
        {
            var response = await _CarService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("{id}")]
        [ActionName("UpdateCar")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] CarUpdateDto dto)
        {
            var response = await _CarService.UpdateAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteCar")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _CarService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPost]
        [ActionName("SetMainImage")]
        public async Task<IActionResult> SetMainImage([FromQuery] string carId, string imageId)
        {
            var response = await _CarService.SetMainImage(carId, imageId);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{imageId}")]
        [ActionName("DeleteImage")]
        public async Task<IActionResult> SetMainImage([FromRoute] string imageId)
        {
            var response = await _CarService.RemoveImage(imageId);

            return StatusCode(response.StatusCode, response);
        }
        [HttpPost("{carId}")]
        [ActionName("FinishAuction")]
        public async Task<IActionResult> FinishAuction([FromRoute] string carId)
        {
            var response = await _CarService.FinishAuction(carId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
