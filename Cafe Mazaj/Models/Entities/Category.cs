using System.ComponentModel.DataAnnotations;

namespace Cafe_Mazaj.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string NameEn { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string NameAr { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Slug { get; set; }

        public int SortOrder { get; set; } = 0;

        // Navigation
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}