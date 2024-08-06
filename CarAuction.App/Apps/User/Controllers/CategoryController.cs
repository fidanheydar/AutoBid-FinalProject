using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        [HttpGet]
        [ActionName("GetCategories")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await categoryService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [ActionName("GetCategory")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await categoryService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
