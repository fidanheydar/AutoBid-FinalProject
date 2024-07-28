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
        private readonly ILogger<CarController> _logger;
        public CarController(ICarService service,IFuelService fuelService, IBanService banService, IColorService colorService, IBrandService brandService, ILogger<CarController> logger)
        {
            _service = service;
            _fuelService = fuelService;
            _banService = banService;
            _colorService = colorService;
            _brandService = brandService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var result = await _service.GetAllAsync(0, 0);
            int TotalCount = ((IEnumerable<CarGetDto>)result.items).Count();
            ViewBag.TotalPage = (int)Math.Ceiling((decimal)TotalCount / 5);
            ViewBag.CurrentPage = page;
            int count = 5;
             result = await _service.GetAllAsync(count,page);
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
            ViewBag.Fuels = resultFuel.items;
            ViewBag.Bans = resultBan.items;
            ViewBag.Colors = resultColor.items;
            ViewBag.Brands = resultBrand.items;
            var result = await _service.GetAsync(id);
            if (result.StatusCode == 404)
            {
                return NotFound();
            }
            return View(result.items);
        }
        [HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Update(string id,CarUpdateDto dto)
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
		//public async Task<IActionResult> SetAsMainImage(int id)
		//{
  //          var result =await _carImageService.GetAsync(id,null);
		//	CarImage carImage = (CarImage)result.itemView;

		//	if (carImage == null)
		//	{
		//		return Json(new { status = 404 });
		//	}

		//	carImage.isMain = true;
		//	result = await _carImageService.GetAsync(id, x => x.isMain && x.CarId == carImage.CarId);
		//	CarImage? carImage1 = (CarImage)result.itemView;
		//	if (carImage1 is not null)
  //          {
  //              carImage1.isMain = false;
  //          }
		//	await _carImageService.Save();
  //          _logger.LogInformation("Car Main Image Updated by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
  //          return Json(new { status = 200 });
		//}
		//public async Task<IActionResult> RemoveImage(int id)
		//{
  //          var result = await _carImageService.GetAllAsync(0, 0, x => !x.IsDeleted && x.Id == id);
  //          CarImage? carImage = ((IEnumerable<CarImage>)result.items).FirstOrDefault();

		//	if (carImage == null)
		//		return Json(new { status = 404, desc = "image not found" });

		//	if (carImage.isMain)
		//		return Json(new { status = 400, desc = "You cannot remove main image" });

		//	carImage.IsDeleted = true;
		//	await _carImageService.Save();
  //          _logger.LogInformation("Car Image Removed by " + User.FindFirstValue(ClaimTypes.NameIdentifier));
  //          return Json(new { status = 200 });
		//}
	}
}
