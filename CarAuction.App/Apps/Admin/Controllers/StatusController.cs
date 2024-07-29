using CarAuction.Service.DTOs.Statuses;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _StatusService;

        public StatusController(IStatusService StatusService)
        {
            _StatusService = StatusService;
        }
        [HttpPost]
        [ActionName("CreateStatus")]
        public async Task<IActionResult> Post([FromForm]StatusPostDto dto)
        {
            var response = await _StatusService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
   
        [HttpPut("{id}")]
        [ActionName("UpdateStatus")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] StatusUpdateDto dto)
        {
            var response = await _StatusService.UpdateAsync(id,dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteStatus")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _StatusService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
