using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Users.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpGet]
        [ActionName("GetTags")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await _tagService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        [ActionName("GetTag")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await _tagService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
