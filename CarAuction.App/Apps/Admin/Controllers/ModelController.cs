using CarAuction.Service.DTOs.Models;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;

        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }
        [HttpPost]
        [ActionName("CreateModel")]
        public async Task<IActionResult> Post([FromForm]ModelPostDto dto)
        {
            var response = await _modelService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("{id}")]
        [ActionName("UpdateModel")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] ModelUpdateDto dto)
        {
            var response = await _modelService.UpdateAsync(id,dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteModel")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _modelService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
