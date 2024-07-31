using AutoMapper;
using CarAuction.Service.DTOs.Fuels;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarAuction.MVC.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class FuelController : Controller
    {
        private readonly IFuelService _service;
        private readonly IMapper _mapper;
        public FuelController(IFuelService service,  IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _service.GetAllAsync(0, 0);
            int TotalCount = ((IEnumerable<FuelGetDto>)result.items).Count();
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
        public async Task<IActionResult> Create(FuelPostDto dto)
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
            return View(_mapper.Map<FuelUpdateDto>(result.items));
        }
        public async Task<IActionResult> Update(string id, FuelUpdateDto dto)
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
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(string id)
        {
            var result = await _service.RemoveAsync(id);
            if (result.StatusCode == 404)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
