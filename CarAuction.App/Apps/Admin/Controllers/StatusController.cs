using CarAuction.Service.DTOs.Statuses;
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
    public class StatusController(IStatusService statusService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]StatusPostDto dto)
        {
            var response = await statusService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
   
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] StatusUpdateDto dto)
        {
            var response = await statusService.UpdateAsync(id,dto);
            return StatusCode(response.StatusCode, response);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await statusService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
