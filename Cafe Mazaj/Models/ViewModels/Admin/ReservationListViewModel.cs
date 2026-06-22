using Cafe_Mazaj.Models.Entities;

namespace Cafe_Mazaj.Models.ViewModels.Admin
{
    public class ReservationListViewModel
    {
        public IEnumerable<Reservation> Reservations { get; set; } = new List<Reservation>();
        public string? StatusFilter { get; set; }
        public int PendingCount { get; set; }
        public int ApprovedCount { get; set; }
        public int RejectedCount { get; set; }
    }
}
