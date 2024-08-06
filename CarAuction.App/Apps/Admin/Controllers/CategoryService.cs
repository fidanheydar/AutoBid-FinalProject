using CarAuction.Service.DTOs.Categories;
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
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]CategoryPostDto dto)
        {
            var response = await categoryService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] CategoryUpdateDto dto)
        {
            var response = await categoryService.UpdateAsync(id,dto);
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await categoryService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
