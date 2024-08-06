using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class TagController(ITagService tagService) : ControllerBase
    {
        [HttpGet]
        [ActionName("GetTags")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await tagService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [ActionName("GetTag")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await tagService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
