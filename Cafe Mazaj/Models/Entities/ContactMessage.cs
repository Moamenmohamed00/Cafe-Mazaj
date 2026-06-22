using System.ComponentModel.DataAnnotations;

namespace Cafe_Mazaj.Models.Entities
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string SenderName { get; set; } = string.Empty;

        [Required, MaxLength(200), EmailAddress]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Phone { get; set; }

        [Required, MaxLength(2000)]
        public string Message { get; set; } = string.Empty;

        public bool IsRead { get; set; } = false;

        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
    }
}
