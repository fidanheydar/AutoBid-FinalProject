using CarAuction.Service.DTOs.Models;
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
    public class ModelController(IModelService modelService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromForm]ModelPostDto dto)
        {
            var response = await modelService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromForm] ModelUpdateDto dto)
        {
            var response = await modelService.UpdateAsync(id,dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var response = await modelService.RemoveAsync(id);
            return StatusCode(response.StatusCode, response);
        }
    }
}
