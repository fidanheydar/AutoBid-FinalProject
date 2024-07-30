using AutoMapper;
using CarAuction.Core.Models;
using CarAuction.Service.DTOs.Cars;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace CarAuction.MVC.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CarController : Controller
    {
        private readonly ICarService _service;
        private readonly IFuelService _fuelService;
        private readonly IBanService _banService;
        private readonly IColorService _colorService;
        private readonly IBrandService _brandService;
        private readonly IModelService _modelService;
        private readonly ILogger<CarController> _logger;
        private readonly IMapper _mapper;
		public CarController(ICarService service, IFuelService fuelService, IBanService banService, IColorService colorService, IBrandService brandService, ILogger<CarController> logger, IModelService modelService, IMapper mapper)
		{
			_service = service;
			_fuelService = fuelService;
			_banService = banService;
			_colorService = colorService;
			_brandService = brandService;
			_logger = logger;
			_modelService = modelService;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _service.GetAllAsync(0, 0, x => x.Status.Level != 3);
            int TotalCount = ((IEnumerable<CarGetDto>)result.items).Count();
            ViewBag.TotalPage = (int)Math.Ceiling((decimal)TotalCount / 5);
            ViewBag.CurrentPage = page;
            int count = 5;
             result = await _service.GetAllAsync(count,page, x => x.Status.Level != 3);
            return View(result.items);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var resultFuel = await _fuelService.GetAllAsync(0, 0);
            var resultBan = await _banService.GetAllAsync(0, 0);
            var resultColor = await _colorService.GetAllAsync(0, 0);
			var resultBrand = await _brandService.GetAllAsync(0, 0);
			ViewBag.Fuels = resultFuel.items;
            ViewBag.Bans = resultBan.items;
            ViewBag.Colors = resultColor.items;
			ViewBag.Brands = resultBrand.items;
			return View();
        }
        [HttpGet]
        public async Task<IActionResult> Archive(int page = 1)
        {
            var result = await _service.GetAllAsync(0, 0,x=>x.Status.Level ==3);
            int TotalCount = ((IEnumerable<CarGetDto>)result.items).Count();
            ViewBag.TotalPage = (int)Math.Ceiling((decimal)TotalCount / 5);
            ViewBag.CurrentPage = page;
            int count = 5;
            result = await _service.GetAllAsync(count, page,x => x.Status.Level == 3);
            return View(result.items);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CarPostDto dto)
        {
			var resultFuel = await _fuelService.GetAllAsync(0, 0);
			var resultBan = await _banService.GetAllAsync(0, 0);
			var resultColor = await _colorService.GetAllAsync(0, 0);
			var resultBrand = await _brandService.GetAllAsync(0, 0);
			ViewBag.Fuels = resultFuel.items;
			ViewBag.Bans = resultBan.items;
			ViewBag.Colors = resultColor.items;
			ViewBag.Brands = resultBrand.items;
			if (!ModelState.IsValid)
            {
                return View(dto);
            }
            var result = await _service.CreateAsync(dto);
            if (result.StatusCode == 404)
            {
                ModelState.AddModelError("", result.Description);
                return View(dto);
            }
            //_logger.LogInformation("Car Created by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var resultFuel = await _fuelService.GetAllAsync(0, 0);
            var resultBan = await _banService.GetAllAsync(0, 0);
            var resultColor = await _colorService.GetAllAsync(0, 0);
            var resultBrand = await _brandService.GetAllAsync(0, 0);
			var resultImage = await _service.GetAllImages(id);
			ViewBag.Fuels = resultFuel.items;
            ViewBag.Bans = resultBan.items;
            ViewBag.Colors = resultColor.items;
            ViewBag.Brands = resultBrand.items;
			ViewBag.Images = resultImage.items;
			var result = await _service.GetAsync(id);
            if (result.StatusCode == 404)
            {
                return NotFound();
            }
            return View(_mapper.Map<CarUpdateDto>(result.items));
        }
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(string id,CarUpdateDto dto)
        {
			var resultFuel = await _fuelService.GetAllAsync(0, 0);
			var resultBan = await _banService.GetAllAsync(0, 0);
			var resultColor = await _colorService.GetAllAsync(0, 0);
			var resultBrand = await _brandService.GetAllAsync(0, 0);
			var resultImage = await _service.GetAllImages(id);
			ViewBag.Fuels = resultFuel.items;
			ViewBag.Bans = resultBan.items;
			ViewBag.Colors = resultColor.items;
			ViewBag.Brands = resultBrand.items;
			ViewBag.Images = resultImage.items;
			if (!ModelState.IsValid)
            {
                return View(dto);
            }
           
			var result = await _service.UpdateAsync(id,dto);
            if (result.StatusCode == 400)
            {
                ModelState.AddModelError("", result.Description);
                return View(dto);
            }
            //_logger.LogInformation("Car Updated by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Remove(string id)
        {
            var result = await _service.RemoveAsync(id);
            if(result.StatusCode == 404)
            {
                return NotFound();
            }
            //_logger.LogInformation("Car Removed by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> FinishAuction(string id)
        {
            try
            {
                var result = await _service.FinishAuction(id);
                if (result.StatusCode == 404)
                {
                    return NotFound();
                }
            }
            catch 
            {
                TempData["Error"] = "Please check car details and try again";
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> GetAllModel()
		{
			var result = await _modelService.GetAllAsync(0, 0);
			return Json(result.items);
		}
        public async Task<IActionResult> SetAsMainImage(string id)
        {
            var result = await _service.GetImage(x=>x.Id.ToString() == id && !x.IsDeleted);
            CarImage carImage = (CarImage)result.items;

            if (carImage == null)
            {
                return Json(new { status = 404 });
            }

            carImage.isMain = true;
            result = await _service.GetImage(x => x.isMain && x.CarId == carImage.CarId);
            CarImage? carImage1 = (CarImage)result.items;
            if (carImage1 is not null)
            {
                carImage1.isMain = false;
            }
            await _service.SaveImage();
            //_logger.LogInformation("Car Main Image Updated by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
            return Json(new { status = 200 });
        }
        public async Task<IActionResult> RemoveImage(string id)
        {
            CarImage carImage = (CarImage)(await _service.GetImage(x => !x.IsDeleted && x.Id.ToString() == id)).items;

            if (carImage == null)
                return Json(new { status = 404, desc = "image not found" });

            if (carImage.isMain)
                return Json(new { status = 400, desc = "You cannot remove main image" });

            carImage.IsDeleted = true;
			await _service.SaveImage();
			//_logger.LogInformation("Car Image Removed by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
			return Json(new { status = 200 });
        }
    }
}
