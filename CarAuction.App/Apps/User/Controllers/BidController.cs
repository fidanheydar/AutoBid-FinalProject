using CarAuction.Service.DTOs.Bids;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Users.Controllers
{
    [ApiExplorerSettings(GroupName = "user_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;

        public BidController(IBidService BidService)
        {
            _bidService = BidService;
        }
        [HttpPost]
        [ActionName("DoBid")]
        [Authorize]
        public async Task<IActionResult> DoBid([FromForm] BidPostDto dto)
        {
            var response = await _bidService.CreateAsync(dto);
            return StatusCode(response.StatusCode, response);
        }
        [HttpGet]
        [ActionName("GetAllBids")]
        public async Task<IActionResult> GetAllBids([FromQuery]string carId)
        {
            var response = await _bidService.GetAllByCar(carId);
            return StatusCode(response.StatusCode, response);
        }
    }
}
