using CarAuction.Service.DTOs.Colors;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }
        [HttpPost]
        [ActionName("CreateColor")]
        public async Task<IActionResult> Post([FromForm]ColorPostDto dto)
        {
            var response = await _colorService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
      
        [HttpPut("{id}")]
        [ActionName("UpdateColor")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] ColorUpdateDto dto)
        {
            var response = await _colorService.UpdateAsync(id,dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteColor")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _colorService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
