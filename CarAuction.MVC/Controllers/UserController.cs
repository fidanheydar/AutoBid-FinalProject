using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Identity;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.MVC.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class UserController : Controller
    {
        private readonly IIdentityService _service;
        private readonly IWebHostEnvironment _evn;
        private readonly ILogger<UserController> _logger;
        public UserController(IIdentityService service, IWebHostEnvironment evn, ILogger<UserController> logger)
        {
            _service = service;
            _evn = evn;
            _logger = logger;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _service.GetAllUsers(0,0,"User");
            int TotalCount = ((IEnumerable<UserGetDto>)result.items).Count();
            ViewBag.TotalPage = (int)Math.Ceiling((decimal)TotalCount / 5);
            ViewBag.CurrentPage = page;
            int count = 5;
            result = await _service.GetAllUsers(count, page,"User");
            List<UserGetDto> users = (List<UserGetDto>)result.items;
            return View(users);
        }
        [HttpGet]
        public async Task<IActionResult> Remove(string id)
        {
            var result = await _service.Remove(id);
            if (result.StatusCode != 203)
            {
                if (result.StatusCode == 404)
                {
                    return NotFound();
                }
                ModelState.AddModelError("", result.Description);
                return RedirectToAction(nameof(Index));

            }
            //_logger.LogInformation("User Removed by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }
    }
}
