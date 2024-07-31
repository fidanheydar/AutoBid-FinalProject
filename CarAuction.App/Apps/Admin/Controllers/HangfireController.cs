using CarAuction.Service.Services.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IAuctionService _auctionService;

        public JobController( IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }

        [HttpPost]
        [HttpPost]
        [ActionName("CreateBackgroudJob")]
        public IActionResult CreateBackgroudJob()
        {
            RecurringJob.AddOrUpdate(() => _auctionService.CheckFinishDate(), "*/1 * * * *");
            return Ok();
        }
    }
}
