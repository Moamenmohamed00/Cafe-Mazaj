using Cafe_Mazaj.Filters;
using Cafe_Mazaj.Models.Entities;
using Cafe_Mazaj.Models.ViewModels.Admin;
using Cafe_Mazaj.Services.Reservation;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Mazaj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservations;
        public ReservationController(IReservationService reservations) => _reservations = reservations;

        public async Task<IActionResult> Index(string? status)
        {
            var all = await _reservations.GetAllAsync();
            var filtered = string.IsNullOrEmpty(status) ? all : all.Where(r => r.Status == status);

            var vm = new ReservationListViewModel
            {
                Reservations     = filtered,
                StatusFilter     = status,
                PendingCount     = all.Count(r => r.Status == ReservationStatus.Pending),
                ApprovedCount    = all.Count(r => r.Status == ReservationStatus.Approved),
                RejectedCount    = all.Count(r => r.Status == ReservationStatus.Rejected)
            };
            return View(vm);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var r = await _reservations.GetByIdAsync(id);
            if (r == null) return NotFound();
            return View(r);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id)
        {
            await _reservations.UpdateStatusAsync(id, ReservationStatus.Approved);
            TempData["Success"] = "Reservation approved.";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(int id)
        {
            await _reservations.UpdateStatusAsync(id, ReservationStatus.Rejected);
            TempData["Success"] = "Reservation rejected.";
            return RedirectToAction("Index");
        }
    }
}
