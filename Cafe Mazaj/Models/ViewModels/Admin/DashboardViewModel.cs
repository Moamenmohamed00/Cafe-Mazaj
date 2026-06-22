using Cafe_Mazaj.Models.Entities;

namespace Cafe_Mazaj.Models.ViewModels.Admin
{
    public class DashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int PendingReservations { get; set; }
        public int TotalReservations { get; set; }
        public int ApprovedReservations { get; set; }
        public int RejectedReservations { get; set; }
        public int UnreadMessages { get; set; }
        public int TotalGalleryImages { get; set; }

        public IEnumerable<Reservation> RecentReservations { get; set; } = new List<Reservation>();
        public IEnumerable<ContactMessage> RecentMessages { get; set; } = new List<ContactMessage>();
    }
}
