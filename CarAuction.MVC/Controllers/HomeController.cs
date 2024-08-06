using CarAuction.Core.Models;
using CarAuction.MVC.ViewModels;
using CarAuction.Service.DTOs.Bids;
using CarAuction.Service.DTOs.Cars;
using CarAuction.Service.DTOs.Identity;
using CarAuction.Service.DTOs.Statuses;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Globalization;

namespace CarAuction.MVC.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class HomeController : Controller
    {
        private readonly IIdentityService _identityService;
        private readonly ICarService _carService;
        private readonly IStatusService _statusService;
        private readonly IBidService _bidService;
        private readonly IBrandService _brandService;

        public HomeController(ICarService carService, IIdentityService identityService, IStatusService statusService, IBidService bidService, IBrandService brandService)
        {
            _carService = carService;
            _identityService = identityService;
            _statusService = statusService;
            _bidService = bidService;
            _brandService = brandService;
        }
        public async Task<IActionResult> DateSearch(string? date, string? todate)
        {
            var result = await _identityService.GetAllUsers(0, 0, "Admin");
            HomeVM vm = new()
            {
                Admins = (IEnumerable<UserGetDto>)result.items
            };

            CultureInfo culture = new CultureInfo("en-US");
            if (date != "null" && todate == "null")
            {
                DateTime tempDate = Convert.ToDateTime(date, culture);
                result = await _carService.GetAllAsync(0, 0, x => x.Status.Level == 3 && x.CarAuctionDetail.FinishDate > tempDate);
                vm.Cars = (IEnumerable<CarGetDto>)result.items;
                result = await _bidService.GetAllAsync(0, 0, x => !x.IsDeleted && x.CreatedAt >= tempDate);
                vm.Bids = (IEnumerable<BidGetDto>)result.items;
                return Json(vm);
            }
            else if (todate != "null" && date == "null")
            {
                DateTime tempDate = Convert.ToDateTime(todate, culture);
                result = await _carService.GetAllAsync(0, 0, x => x.Status.Level == 3 && x.CarAuctionDetail.FinishDate < tempDate);
                vm.Cars = (IEnumerable<CarGetDto>)result.items;
                result = await _bidService.GetAllAsync(0, 0, x => !x.IsDeleted && x.CreatedAt <= tempDate);
                vm.Bids = (IEnumerable<BidGetDto>)result.items;
                return Json(vm);
            }
            else if (todate == "null" && date == "null")
            {
                return Json(vm);
            }
            else
            {
                DateTime tempDate = Convert.ToDateTime(date, culture);
                DateTime tempDate1 = Convert.ToDateTime(todate, culture);
                result = await _carService.GetAllAsync(0, 0, x => x.Status.Level == 3 && x.CarAuctionDetail.FinishDate > tempDate && x.CarAuctionDetail.FinishDate < tempDate1);
                vm.Cars = (IEnumerable<CarGetDto>)result.items;
                result = await _bidService.GetAllAsync(0, 0, x => !x.IsDeleted && x.CreatedAt > tempDate && x.CreatedAt < tempDate1);
                vm.Bids = (IEnumerable<BidGetDto>)result.items;
                return Json(vm);
            }
        }

        public async Task<IActionResult> Index(string? date, string? todate)
        {
            var result = await _identityService.GetAllUsers(0, 0, "Admin");
            HomeVM vm = new()
            {
                Admins = (IEnumerable<UserGetDto>)result.items
            };
            result = await _carService.GetAllAsync(0, 0, x => x.Status.Level != 1);
            vm.Cars = (IEnumerable<CarGetDto>)result.items;
            result = await _statusService.GetAllAsync(0, 0);
            vm.Statuses = (IEnumerable<StatusGetDto>)result.items;
            result = await _bidService.GetAllAsync(0, 0, null);
            vm.Bids = (IEnumerable<BidGetDto>)result.items;
            result = await _identityService.GetAllUsers(0, 0, "User");
            vm.Users = (IEnumerable<UserGetDto>)result.items;
            var chartResult = await _brandService.GetChartData();
            vm.ChartData = chartResult;
            return View(vm);
        }
        public async Task<IActionResult> Date(string? date)
        {
            CultureInfo culture = new CultureInfo("en-US");
            DateTime tempDate = Convert.ToDateTime(date, culture);
            if (date is null)
            {
                return Json(null);
            }
            return Json(tempDate);
        }
        public async Task<IActionResult> Search(string search)
        {
            var result = await _carService.GetAllAsync(0, 0, x => x.Status.Level != 3 && !x.IsDeleted && ((x.Model.Name.Trim().ToLower() + " " + x.Model.Brand.Name.Trim().ToLower()).Contains(search.Trim().ToLower()) || (x.Model.Brand.Name.Trim().ToLower() + " " + x.Model.Name.Trim().ToLower()).Contains(search.Trim().ToLower()) || (x.Vin.Trim().ToLower()).Contains(search.Trim().ToLower())));

            IEnumerable<CarGetDto> Cars = ((IEnumerable<CarGetDto>)result.items);
            return Json(Cars);
        }
    }
}
