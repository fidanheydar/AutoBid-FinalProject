using CarAuction.Service.DTOs.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAuction.Service.Services.Interfaces
{
    public interface IMailService
    {
        Task SendEmailAsync(MailRequestDTO mailRequest);
    }
}
