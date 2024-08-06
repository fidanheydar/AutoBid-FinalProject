using CarAuction.Service.DTOs.Bans;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class BanController(IBanService banService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]BanPostDto dto)
        {
            var response = await banService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] BanUpdateDto dto)
        {
            var response = await banService.UpdateAsync(id,dto);
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await banService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
