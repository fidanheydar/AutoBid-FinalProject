using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BlogController(IBlogService blogService) : ControllerBase
    {
        [HttpGet]
        [ActionName("GetBlogs")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await blogService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [ActionName("GetBlog")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await blogService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
