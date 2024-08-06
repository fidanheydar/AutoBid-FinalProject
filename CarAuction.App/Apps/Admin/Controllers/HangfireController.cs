using CarAuction.Service.Services.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
    public class JobController(IAuctionService auctionService) : ControllerBase
    {
        [HttpPost]
        [HttpPost]
        [ActionName("CreateBackgroudJob")]
        public IActionResult CreateBackgroudJob()
        {
            RecurringJob.AddOrUpdate(() => auctionService.CheckFinishDate(), "*/1 * * * *");
            return Ok();
        }
    }
}
