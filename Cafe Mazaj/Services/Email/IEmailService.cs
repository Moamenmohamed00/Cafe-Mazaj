namespace Cafe_Mazaj.Services.Email
{
    public class EmailSettings
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string SenderEmail { get; set; } = string.Empty;
        public string SenderName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public interface IEmailService
    {
        Task SendAsync(string toEmail, string subject, string htmlBody);
    }
}
