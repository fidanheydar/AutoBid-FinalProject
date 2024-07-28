using AutoMapper;
using CarAuction.Service.DTOs.Tags;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarAuction.MVC.Controllers
{
    //[Area("Admin")]
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class TagController : Controller
    {
        private readonly ITagService _service;
        private readonly ILogger<TagController> _logger;
        private readonly IMapper _mapper;
        public TagController(ITagService service, ILogger<TagController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _service.GetAllAsync(0, 0);
            int TotalCount = ((IEnumerable<TagGetDto>)result.items).Count();
            ViewBag.TotalPage = (int)Math.Ceiling((decimal)TotalCount / 8);
            ViewBag.CurrentPage = page;
            int count = 8;
             result = await _service.GetAllAsync(count, page);
            return View(result.items);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagPostDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var result = await _service.CreateAsync(dto);
            if (result.StatusCode == 400)
            {
                ModelState.AddModelError("", result.Description);
                return View(dto);
            }
            //_logger.LogInformation("Tag Created by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var result = await _service.GetAsync(id);
            if (result.StatusCode == 404)
            {
                return NotFound();
            }
            return View(_mapper.Map<TagUpdateDto>(result.items));
        }
        public async Task<IActionResult> Update(string id, TagUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var result = await _service.UpdateAsync(id, dto);
            if (result.StatusCode == 400)
            {
                ModelState.AddModelError("", result.Description);
                return View(dto);
            }
            //_logger.LogInformation("Tag Updated by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(string id)
        {
            var result = await _service.RemoveAsync(id);
            if (result.StatusCode == 404)
            {
                return NotFound();
            }
            //_logger.LogInformation("Tag Removed by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }
    }
}
