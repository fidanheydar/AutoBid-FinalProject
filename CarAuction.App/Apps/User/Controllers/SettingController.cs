using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Users.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly ISettingService _SettingService;

        public SettingController(ISettingService SettingService)
        {
            _SettingService = SettingService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _SettingService.GetAllAsync(0, 0);
            return StatusCode(response.StatusCode, response);
        }
    }
}
