using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ModelController(IModelService modelService) : ControllerBase
    {
        [HttpGet]
        [ActionName("GetModels")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await modelService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        [ActionName("GetModel")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await modelService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
