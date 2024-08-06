using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BrandController(IBrandService brandService) : ControllerBase
    {
        [HttpGet]
        [ActionName("GetBrands")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await brandService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [ActionName("GetBrand")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await brandService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
