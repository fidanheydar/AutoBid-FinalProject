using CarAuction.Service.DTOs.Blogs;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        [HttpPost]
        [ActionName("CreateBlog")]
        public async Task<IActionResult> Post([FromForm]BlogPostDto dto)
        {
            var response = await _blogService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("{id}")]
        [ActionName("UpdateBlog")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] BlogUpdateDto dto)
        {
            var response = await _blogService.UpdateAsync(id,dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteBlog")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _blogService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
