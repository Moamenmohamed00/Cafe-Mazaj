using System.ComponentModel.DataAnnotations;

namespace Cafe_Mazaj.Models.Entities
{
    public class GalleryImage
    {
        public int Id { get; set; }

        [Required, MaxLength(500)]
        public string ImagePath { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? AltTextEn { get; set; }

        [MaxLength(200)]
        public string? AltTextAr { get; set; }

        [MaxLength(100)]
        public string? Tag { get; set; } // Food / Drinks / Ambiance

        public int SortOrder { get; set; } = 0;

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
