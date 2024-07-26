using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Users.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class SubscribeController : ControllerBase
    {
        private readonly ISubscribeService _subcribeService;

        public SubscribeController(ISubscribeService subscribeService)
        {
            _subcribeService = subscribeService;
        }
        [HttpPost]
        [ActionName("ApplySubscribe")]
        public async Task<IActionResult> Get(string email)
        {
            var response = await _subcribeService.CreateAsync(email);
            return StatusCode(response.StatusCode, response);
        }
    }
}
