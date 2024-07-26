using CarAuction.Service.DTOs.Tags;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [HttpPost]
        [ActionName("CreateTag")]
        public async Task<IActionResult> Post([FromForm]TagPostDto dto)
        {
            var response = await _tagService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
      
        [HttpPut("{id}")]
        [ActionName("UpdateTag")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] TagUpdateDto dto)
        {
            var response = await _tagService.UpdateAsync(id,dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteTag")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _tagService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
