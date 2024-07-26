using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Users.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpGet]
        [ActionName("GetBlogs")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await _blogService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        [ActionName("GetBlog")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await _blogService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
