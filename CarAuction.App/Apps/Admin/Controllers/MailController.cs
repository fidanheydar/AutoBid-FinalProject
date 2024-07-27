using CarAuction.Service.DTOs.Mail;
using CarAuction.Service.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarAuction.App.Apps.Admin.Controllers
{
    [ApiExplorerSettings(GroupName = "admin_v1")]
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        readonly IMailService _mailService;

        public MailController(IMailService mailService)
        {
            _mailService = mailService;
        }

        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail([FromForm] MailRequestDTO request)
        {
            await _mailService.SendEmailAsync(request);
            return Ok();
        }
    }
}
