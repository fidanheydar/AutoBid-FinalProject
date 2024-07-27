using CarAuction.Core.Options;
using CarAuction.Service.DTOs.Mail;
using CarAuction.Service.Services.Interfaces;
using MailKit.Security;
using MimeKit;
using System.Net.Mail;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using Microsoft.Extensions.Options;

namespace CarAuction.Service.Services.Abstractions
{
    public class MailService : IMailService
    {
        private readonly MailSetting _mailSetting;

        public MailService(IOptionsMonitor<MailSetting> mailSetting)
        {
            _mailSetting = mailSetting.CurrentValue;
        }

        public async Task SendEmailAsync(MailRequestDTO mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_mailSetting.Mail);
            foreach (var ToEmail in mailRequest.ToEmails)
            {
                email.To.Add(MailboxAddress.Parse(ToEmail));
            }
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSetting.Host, _mailSetting.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSetting.Mail, _mailSetting.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
