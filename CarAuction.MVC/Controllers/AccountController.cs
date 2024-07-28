using CarAuction.Service.DTOs.Identity;
using CarAuction.Service.Responses;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.Service.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService, IAuthService authService)
        {
            _identityService = identityService;
            _authService = authService;
        }

        //[Authorize]
        //public async Task<IActionResult> Info()
        //{
        //    var result = await _service.Info();
        //    if (result.StatusCode != 200)
        //    {
        //        TempData["AdminInfo"] = result.Description;
        //        return RedirectToAction("index", "home");
        //    }
        //    InfoVM info = (InfoVM)result.items;
        //    return View(info);
        //}
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            ApiResponse result = new ApiResponse();
            try
            {
                 result = await _identityService.Login(dto, 200, "Admin");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
            return RedirectToAction("index", "home");
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
             await _identityService.Logout();
            return RedirectToAction("index", "home");
        }
        //[Authorize]
        //public async Task<IActionResult> Update()
        //{
        //    var resultCountry = await _countryService.GetAllAsync(0, 0);
        //    ViewBag.Countries = resultCountry.items;
        //    var result = await _service.GetUser();
        //    if (result.StatusCode != 203)
        //    {
        //        return NotFound();
        //    }
        //    AppUser user = (AppUser)result.items;
        //    UpdateDto dto = new UpdateDto()
        //    {
        //        UserName = user.UserName,
        //        Email = user.Email,
        //        Name = user.Name,
        //        Surname = user.Surname,
        //        CountryId = user.CountryId,
        //        Image = user.Image
        //    };
        //    return View(dto);
        //}
        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> Update(UpdateDto dto)
        //{
        //    var resultCountry = await _countryService.GetAllAsync(0, 0);
        //    ViewBag.Countries = resultCountry.items;
        //    if (!ModelState.IsValid)
        //    {
        //        return View(dto);
        //    }
        //    var result = await _service.Update(dto, null);
        //    if (result.StatusCode != 203)
        //    {
        //        if (result.StatusCode == 404)
        //        {
        //            return NotFound();
        //        }
        //        ModelState.AddModelError("", result.Description);
        //        return View(dto);
        //    }
        //    return RedirectToAction(nameof(Info));
        //}
        //public async Task<IActionResult> AdminCreate()
        //{
        //    AppUser SuperAdmin = new AppUser
        //    {
        //        Name = "SuperAdmin",
        //        Surname = "SuperAdmin",
        //        Email = "superadmin@miles.com",
        //        UserName = "SuperAdmin",
        //        Image = "fbcfba13-c108-4de8-ba04-dd8372b1fa78nijat.jpg",
        //        CountryId = 1,
        //        UserPricingId = 3,
        //        EmailConfirmed = true
        //    };
        //    await _userManager.CreateAsync(SuperAdmin, "Admin123.");
        //    AppUser Admin = new AppUser
        //    {
        //        Name = "Admin",
        //        Surname = "Admin",
        //        Email = "admin@miles.com",
        //        UserName = "Admin",
        //        Image  = "fbcfba13-c108-4de8-ba04-dd8372b1fa78nijat.jpg",
        //        CountryId = 1,
        //        UserPricingId = 3,
        //        EmailConfirmed = true
        //    };
        //    await _userManager.CreateAsync(Admin, "Admin123.");

        //    await _userManager.AddToRoleAsync(SuperAdmin, "SuperAdmin");
        //    await _userManager.AddToRoleAsync(Admin, "Admin");
        //    return Json("ok");
        //}
        //public async Task<IActionResult> AddRole()
        //{
        //    IdentityRole role = new IdentityRole
        //    {
        //        Name = "User"
        //    };
        //    IdentityRole role1 = new IdentityRole
        //    {
        //        Name = "Admin"
        //    };
        //    IdentityRole role2 = new IdentityRole
        //    {
        //        Name = "SuperAdmin"
        //    };
        //   await _roleManager.CreateAsync(role);
        //   await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    return RedirectToAction("index", "home");
        //}
    }
}
