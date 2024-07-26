using CarAuction.Service.DTOs.Brands;
using CarAuction.Service.DTOs.Tags;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpPost]
        [ActionName("CreateBrand")]
        public async Task<IActionResult> Post([FromForm]BrandPostDto dto)
        {
            var response = await _brandService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("{id}")]
        [ActionName("UpdateBrand")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] BrandUpdateDto dto)
        {
            var response = await _brandService.UpdateAsync(id,dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteBrand")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _brandService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
