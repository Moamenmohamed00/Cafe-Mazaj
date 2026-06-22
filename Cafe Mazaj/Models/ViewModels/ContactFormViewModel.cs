using System.ComponentModel.DataAnnotations;

namespace Cafe_Mazaj.Models.ViewModels
{
    public class ContactFormViewModel
    {
        [Required(ErrorMessage = "Name is required / الاسم مطلوب")]
        [MaxLength(200)]
        [Display(Name = "Full Name")]
        public string SenderName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required / البريد الإلكتروني مطلوب")]
        [EmailAddress]
        [MaxLength(200)]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        [Display(Name = "Phone (optional)")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Message is required / الرسالة مطلوبة")]
        [MaxLength(2000)]
        [Display(Name = "Message")]
        public string Message { get; set; } = string.Empty;
    }
}
