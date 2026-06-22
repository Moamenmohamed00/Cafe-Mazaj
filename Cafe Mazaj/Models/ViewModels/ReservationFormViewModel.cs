using System.ComponentModel.DataAnnotations;

namespace Cafe_Mazaj.Models.ViewModels
{
    public class ReservationFormViewModel
    {
        [Required(ErrorMessage = "Name is required / الاسم مطلوب")]
        [MaxLength(200)]
        [Display(Name = "Full Name")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone is required / الهاتف مطلوب")]
        [MaxLength(20)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Date is required / التاريخ مطلوب")]
        [Display(Name = "Reservation Date")]
        public DateOnly ReservationDate { get; set; } = DateOnly.FromDateTime(DateTime.Today.AddDays(1));

        [Required]
        [Range(1, 50, ErrorMessage = "Guests must be between 1 and 50")]
        [Display(Name = "Number of Guests")]
        public int GuestCount { get; set; } = 2;

        [MaxLength(500)]
        [Display(Name = "Special Requests")]
        public string? Notes { get; set; }
    }
}
