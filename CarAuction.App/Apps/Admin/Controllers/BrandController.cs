using CarAuction.Service.DTOs.Brands;
using CarAuction.Service.DTOs.Tags;
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
    public class BrandController(IBrandService brandService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]BrandPostDto dto)
        {
            var response = await brandService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] BrandUpdateDto dto)
        {
            var response = await brandService.UpdateAsync(id,dto);
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await brandService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
