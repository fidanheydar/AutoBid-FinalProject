using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CarAuction.Service.Services.Interfaces;
using AutoMapper;
using CarAuction.Service.DTOs.Statuses;

namespace CarAuction.MVC.Controllers
{
	[Authorize(Roles = "Admin,SuperAdmin")]
	public class StatusController : Controller
	{
		private readonly IStatusService _service;
		private readonly ILogger<StatusController> _logger;
		private readonly IMapper _mapper;

		public StatusController(IStatusService service, ILogger<StatusController> logger, IMapper mapper)
		{
			_service = service;
			_logger = logger;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index(int page = 1)
		{
			var result = await _service.GetAllAsync(0, 0);
			int TotalCount = ((IEnumerable<StatusGetDto>)result.items).Count();
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
		public async Task<IActionResult> Create(StatusPostDto dto)
		{
			if (!ModelState.IsValid)
			{
				return View(dto);
			}
			try
			{
				var result = await _service.CreateAsync(dto);
				if (result.StatusCode == 400)
				{
					ModelState.AddModelError("", result.Description);
					return View(dto);
				}
				//_logger.LogInformation("Status Created by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("Level", "Level must be unique");
				return View(dto);
			}
		}
		[HttpGet]
		public async Task<IActionResult> Update(string id)
		{
			var result = await _service.GetAsync(id);
			if (result.StatusCode == 404)
			{
				return NotFound();
			}
			return View(_mapper.Map<StatusUpdateDto>(result.items));
		}
		public async Task<IActionResult> Update(string id, StatusUpdateDto dto)
		{
			if (!ModelState.IsValid)
			{
				return View(dto);
			}
			try
			{
				var result = await _service.UpdateAsync(id, dto);
				if (result.StatusCode == 400)
				{
					ModelState.AddModelError("", result.Description);
					return View(dto);
				}
				//_logger.LogInformation("Status Updated by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("Level", "Level must be unique");
				return View(dto);
			}
		}
		public async Task<IActionResult> Remove(string id)
		{
			var result = await _service.RemoveAsync(id);
			if (result.StatusCode == 404)
			{
				return NotFound();
			}
			//_logger.LogInformation("Status Removed by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
			return RedirectToAction(nameof(Index));
		}
	}
}
