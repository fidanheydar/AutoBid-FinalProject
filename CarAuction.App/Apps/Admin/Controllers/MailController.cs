using CarAuction.Service.DTOs.Mail;
using CarAuction.Service.Services.Interfaces;
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
    public class MailController(IMailService mailService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> SendMail([FromForm] MailRequestDTO request)
        {
            await mailService.SendEmailAsync(request);
            return Ok();
        }
    }
}
