using Cafe_Mazaj.Models.Entities;
using Cafe_Mazaj.Models.ViewModels;
using Cafe_Mazaj.Services.ContactMessage;
using Cafe_Mazaj.Services.Email;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Mazaj.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contact;
        private readonly IEmailService _email;
        private readonly string _adminEmail;

        public ContactController(IContactService contact, IEmailService email, IConfiguration config)
        {
            _contact = contact;
            _email = email;
            _adminEmail = config["AdminSettings:NotificationEmail"] ?? string.Empty;
        }

        [HttpGet]
        public IActionResult Index() => View(new ContactFormViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactFormViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var message = new Models.Entities.ContactMessage
            {
                SenderName = vm.SenderName,
                Email      = vm.Email,
                Phone      = vm.Phone,
                Message    = vm.Message
            };

            await _contact.CreateAsync(message);

            // Notify admin
            await _email.SendAsync(
                _adminEmail,
                "📬 New Contact Message — Cafe Mazaj",
                $"<h2>New Message from {message.SenderName}</h2>" +
                $"<p><b>Email:</b> {message.Email}</p>" +
                $"<p><b>Phone:</b> {message.Phone ?? "-"}</p>" +
                $"<p><b>Message:</b><br/>{message.Message}</p>");

            TempData["Success"] = "Message sent! We'll get back to you soon.";
            return RedirectToAction("Index");
        }
    }
}
