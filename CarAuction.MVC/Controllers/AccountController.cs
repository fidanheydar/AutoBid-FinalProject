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
        [Authorize]
        public async Task<IActionResult> Update()
        {
            var result = await _identityService.GetAllUsers(0, 0, "Admin");
            List<UserGetDto> admins = (List<UserGetDto>)result.items;

            try
            {
                var user = admins.FirstOrDefault(x => x.UserName == User.Identity.Name);

                UpdateDto dto = new UpdateDto()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.Surname,
                };
                return View(dto);

            }
            catch
            {
                await _identityService.Logout();
                return RedirectToAction("login", "account");
            }
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(UpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                var result = await _identityService.UpdateUser(dto);
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
                return RedirectToAction("index", "home");

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
        }
    }
}
