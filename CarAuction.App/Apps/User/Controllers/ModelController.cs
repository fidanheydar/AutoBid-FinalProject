using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Users.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;

        public ModelController(IModelService modelService)
        {
            _modelService = modelService;
        }
        [HttpGet]
        [ActionName("GetModels")]
        public async Task<IActionResult> Get([FromQuery] int page, int count)
        {
            var response = await _modelService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet("{id}")]
        [ActionName("GetModel")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var response = await _modelService.GetAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
