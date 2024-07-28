using CarAuction.Core.Models;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarAuction.MVC.Controllers
{
    //[Area("Admin")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class SubscribeController : Controller
    {
        private readonly ISubscribeService _service;
        private readonly ILogger<SubscribeController> _logger;
        public SubscribeController(ISubscribeService service, ILogger<SubscribeController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _service.GetAllAsync(0, 0);
            int TotalCount = ((IEnumerable<Subscribe>)result.items).Count();
            ViewBag.TotalPage = (int)Math.Ceiling((decimal)TotalCount / 8);
            ViewBag.CurrentPage = page;
            int count = 8;
            result = await _service.GetAllAsync(count,page);
            return View(result.items);
        }
   
        [HttpGet]
        public async Task<IActionResult> Remove(string id)
        {
            var result = await _service.RemoveAsync(id);
            if(result.StatusCode == 404)
            {
                return NotFound();
            }
            //_logger.LogInformation("Subscribe Removed by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }
    }
}
