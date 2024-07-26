using CarAuction.Service.DTOs.Categories;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost]
        [ActionName("CreateCategory")]
        public async Task<IActionResult> Post([FromForm]CategoryPostDto dto)
        {
            var response = await _categoryService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("{id}")]
        [ActionName("UpdateCategory")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] CategoryUpdateDto dto)
        {
            var response = await _categoryService.UpdateAsync(id,dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteCategory")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _categoryService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
