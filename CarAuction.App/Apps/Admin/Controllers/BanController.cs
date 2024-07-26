using CarAuction.Service.DTOs.Bans;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BanController : ControllerBase
    {
        private readonly IBanService _banService;

        public BanController(IBanService banService)
        {
            _banService = banService;
        }
        [HttpPost]
        [ActionName("CreateBan")]
        public async Task<IActionResult> Post([FromForm]BanPostDto dto)
        {
            var response = await _banService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("{id}")]
        [ActionName("UpdateBan")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] BanUpdateDto dto)
        {
            var response = await _banService.UpdateAsync(id,dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        [ActionName("DeleteBan")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await _banService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
