using System.Net;
using System.Net.Mail;
using Cafe_Mazaj.Services.Email;
using Microsoft.Extensions.Options;

namespace Cafe_Mazaj.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
            => _settings = settings.Value;

        public async Task SendAsync(string toEmail, string subject, string htmlBody)
        {
            try
            {
                using var client = new SmtpClient(_settings.Host, _settings.Port)
                {
                    EnableSsl = _settings.EnableSsl,
                    Credentials = new NetworkCredential(_settings.SenderEmail, _settings.Password)
                };

                var mail = new MailMessage
                {
                    From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                    Subject = subject,
                    Body = htmlBody,
                    IsBodyHtml = true
                };
                mail.To.Add(toEmail);

                await client.SendMailAsync(mail);
            }
            catch
            {
                // Log silently — email failure shouldn't break the user flow
            }
        }
    }
}
