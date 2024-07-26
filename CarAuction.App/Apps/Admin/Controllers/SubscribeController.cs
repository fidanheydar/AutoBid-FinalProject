using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
        private readonly ISubscribeService _SubscribeService;

        public SubscribeController(ISubscribeService SubscribeService)
        {
            _SubscribeService = SubscribeService;
        }
        [HttpGet]
        [ActionName("GetSubscribers")]
        public async Task<IActionResult> Get([FromQuery]int page,int count)
        {
            var response = await _SubscribeService.GetAllAsync(count, page);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteSubscriber")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _SubscribeService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
