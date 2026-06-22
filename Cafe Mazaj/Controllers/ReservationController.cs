using Cafe_Mazaj.Models.Entities;
using Cafe_Mazaj.Models.ViewModels;
using Cafe_Mazaj.Services.Email;
using Cafe_Mazaj.Services.Reservation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Cafe_Mazaj.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservations;
        private readonly IEmailService _email;
        private readonly string _adminEmail;

        public ReservationController(
            IReservationService reservations,
            IEmailService email,
            IConfiguration config)
        {
            _reservations = reservations;
            _email = email;
            _adminEmail = config["AdminSettings:NotificationEmail"] ?? string.Empty;
        }

        [HttpGet]
        public IActionResult Index() => View(new ReservationFormViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ReservationFormViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);

            var reservation = new Models.Entities.Reservation
            {
                CustomerName    = vm.CustomerName,
                Phone           = vm.Phone,
                ReservationDate = vm.ReservationDate,
                GuestCount      = vm.GuestCount,
                Notes           = vm.Notes,
                Status          = ReservationStatus.Pending
            };

            await _reservations.CreateAsync(reservation);

            // Notify admin
            await _email.SendAsync(
                _adminEmail,
                "🔔 New Reservation — Cafe Mazaj",
                $"<h2>New Reservation</h2>" +
                $"<p><b>Name:</b> {reservation.CustomerName}</p>" +
                $"<p><b>Phone:</b> {reservation.Phone}</p>" +
                $"<p><b>Date:</b> {reservation.ReservationDate}</p>" +
                $"<p><b>Guests:</b> {reservation.GuestCount}</p>" +
                $"<p><b>Notes:</b> {reservation.Notes ?? "-"}</p>");

            TempData["Success"] = "Reservation submitted! We'll contact you shortly.";
            return RedirectToAction("Index");
        }
    }
}
