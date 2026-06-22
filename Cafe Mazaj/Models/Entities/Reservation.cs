using System.ComponentModel.DataAnnotations;

namespace Cafe_Mazaj.Models.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string CustomerName { get; set; } = string.Empty;

        [Required, MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public DateOnly ReservationDate { get; set; }

        [Required, Range(1, 50)]
        public int GuestCount { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = ReservationStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    public static class ReservationStatus
    {
        public const string Pending  = "Pending";
        public const string Approved = "Approved";
        public const string Rejected = "Rejected";
    }
}
