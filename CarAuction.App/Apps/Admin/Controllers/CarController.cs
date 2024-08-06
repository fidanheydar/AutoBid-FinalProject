using CarAuction.Service.DTOs.Cars;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class CarController(ICarService carService, IAuctionService auctionService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CarPostDto dto)
        {
            var response = await carService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] CarUpdateDto dto)
        {
            var response = await carService.UpdateAsync(id, dto);
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await carService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpPost]
        public async Task<IActionResult> SetMainImage([FromQuery] string carId, string imageId)
        {
            var response = await carService.SetMainImage(carId, imageId);
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpDelete("{imageId}")]
        public async Task<IActionResult> DeleteImage([FromRoute] string imageId)
        {
            var response = await carService.RemoveImage(imageId);

            return StatusCode(response.StatusCode, response);
        }
        
        [HttpPost("{carId}")]
        public async Task<IActionResult> FinishAuction([FromRoute] string carId)
        {
            var response = await auctionService.FinishAuction(carId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
