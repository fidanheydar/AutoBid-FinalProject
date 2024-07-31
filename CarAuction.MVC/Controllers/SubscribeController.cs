using CarAuction.Core.Models;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarAuction.MVC.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class SubscribeController : Controller
    {
        private readonly ISubscribeService _service;
        public SubscribeController(ISubscribeService service)
        {
            _service = service;
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
            return RedirectToAction(nameof(Index));
        }
    }
}
