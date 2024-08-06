using CarAuction.Service.DTOs.Bids;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.User.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BidController(IBidService bidService) : ControllerBase
    {
        [HttpPost]
        [ActionName("DoBid")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "User")]
        public async Task<IActionResult> DoBid([FromForm] BidPostDto dto)
        {
            var response = await bidService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet]
        [ActionName("GetAllBids")]
        public async Task<IActionResult> GetAllBids([FromQuery]string carId)
        {
            var response = await bidService.GetAllByCar(carId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
