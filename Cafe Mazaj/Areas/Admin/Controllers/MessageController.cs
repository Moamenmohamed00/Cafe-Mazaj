using Cafe_Mazaj.Filters;
using Cafe_Mazaj.Services.ContactMessage;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Mazaj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class MessageController : Controller
    {
        private readonly IContactService _contact;
        public MessageController(IContactService contact) => _contact = contact;

        public async Task<IActionResult> Index()
        {
            var messages = await _contact.GetAllAsync();
            return View(messages);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var msg = await _contact.GetByIdAsync(id);
            if (msg == null) return NotFound();
            await _contact.MarkAsReadAsync(id);
            return View(msg);
        }
    }
}
