using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Users.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet]
        [ActionName("GetBrands")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await _brandService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        [ActionName("GetBrand")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await _brandService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
