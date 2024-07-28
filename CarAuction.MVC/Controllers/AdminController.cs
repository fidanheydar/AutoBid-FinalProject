using CarAuction.Service.DTOs.Identity;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarAuction.MVC.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly IIdentityService _service;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IIdentityService service, ILogger<AdminController> logger)
        {
            _service = service;
            _logger = logger;
        }
        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _service.GetAllUsers(0, 0, "Admin");
            int TotalCount = ((IEnumerable<UserGetDto>)result.items).Count();
            ViewBag.TotalPage = (int)Math.Ceiling((decimal)TotalCount / 5);
            ViewBag.CurrentPage = page;
            int count = 5;
            result = await _service.GetAllUsers(count, page, "Admin");
            List<UserGetDto> admins = (List<UserGetDto>)result.items;
            return View(admins);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                var response = await _service.Register(dto, "Admin");
                //_logger.LogInformation("Admin Created by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }
        public async Task<IActionResult> Update(string id)
        {
            var result = await _service.GetAllUsers(0, 0,"Admin");
            List<UserGetDto> admins = (List<UserGetDto>)result.items;


            var user = admins.FirstOrDefault(x => x.Id == id);
            UpdateDto dto = new UpdateDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Name = user.Name,
                Surname = user.Surname,
            };
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> Update(string id, UpdateDto dto)
        {
         
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                var result = await _service.UpdateUser(dto,id);
                if (result.StatusCode != 200)
                {
                    if (result.StatusCode == 404)
                    {
                        return NotFound();
                    }
                    ModelState.AddModelError("", result.Description);
                    return View(dto);
                }
                //_logger.LogInformation("Admin Updated by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }
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
            //_logger.LogInformation("Admin Removed by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }
    }
}
