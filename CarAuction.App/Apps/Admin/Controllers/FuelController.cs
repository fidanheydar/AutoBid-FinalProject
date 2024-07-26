using CarAuction.Service.DTOs.Fuels;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class FuelController : ControllerBase
    {
        private readonly IFuelService _fuelService;

        public FuelController(IFuelService fuelService)
        {
            _fuelService = fuelService;
        }
        [HttpPost]
        [ActionName("CreateFuel")]
        public async Task<IActionResult> Post([FromForm]FuelPostDto dto)
        {
            var response = await _fuelService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
   
        [HttpPut("{id}")]
        [ActionName("UpdateFuel")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] FuelUpdateDto dto)
        {
            var response = await _fuelService.UpdateAsync(id,dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteFuel")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _fuelService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
