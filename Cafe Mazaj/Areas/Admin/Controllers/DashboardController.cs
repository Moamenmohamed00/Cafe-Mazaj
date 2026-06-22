using Cafe_Mazaj.Data;
using Cafe_Mazaj.Filters;
using Cafe_Mazaj.Models.ViewModels.Admin;
using Cafe_Mazaj.Services.ContactMessage;
using Cafe_Mazaj.Services.Gallery;
using Cafe_Mazaj.Services.Product;
using Cafe_Mazaj.Services.Reservation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Mazaj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class DashboardController : Controller
    {
        private readonly IProductService _products;
        private readonly IReservationService _reservations;
        private readonly IContactService _contact;
        private readonly IGalleryService _gallery;
        private readonly AppDbContext _db;

        public DashboardController(
            IProductService products,
            IReservationService reservations,
            IContactService contact,
            IGalleryService gallery,
            AppDbContext db)
        {
            _products = products;
            _reservations = reservations;
            _contact = contact;
            _gallery = gallery;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var allReservations = await _reservations.GetAllAsync();
            var allMessages     = await _contact.GetAllAsync();
            var allGallery      = await _gallery.GetAllAsync();
            var allProducts     = await _products.GetAllAsync();

            var vm = new DashboardViewModel
            {
                TotalProducts         = allProducts.Count(),
                TotalReservations     = allReservations.Count(),
                PendingReservations   = allReservations.Count(r => r.Status == "Pending"),
                ApprovedReservations  = allReservations.Count(r => r.Status == "Approved"),
                RejectedReservations  = allReservations.Count(r => r.Status == "Rejected"),
                UnreadMessages        = allMessages.Count(m => !m.IsRead),
                TotalGalleryImages    = allGallery.Count(),
                RecentReservations    = allReservations.Take(5),
                RecentMessages        = allMessages.Take(5)
            };
            return View(vm);
        }
    }
}
