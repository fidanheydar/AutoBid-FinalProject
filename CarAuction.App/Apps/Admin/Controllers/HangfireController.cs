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
        private readonly ICarService _carService;

        public JobController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpPost]
        [HttpPost]
        [ActionName("CreateBackgroudJob")]
        public IActionResult CreateBackgroudJob()
        {
            RecurringJob.AddOrUpdate(() => _carService.CheckFinishDate(), "*/1 * * * *");
            return Ok();
        }
    }
}
