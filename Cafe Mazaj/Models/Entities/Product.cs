using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cafe_Mazaj.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string NameEn { get; set; } = string.Empty;

        [Required, MaxLength(200)]
        public string NameAr { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? DescriptionEn { get; set; }

        [MaxLength(500)]
        public string? DescriptionAr { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [MaxLength(500)]
        public string? ImagePath { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public bool IsAvailable { get; set; } = true;
        public bool IsFeatured { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
