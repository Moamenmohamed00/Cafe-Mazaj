using System.ComponentModel.DataAnnotations;
using Cafe_Mazaj.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cafe_Mazaj.Models.ViewModels.Admin
{
    public class ProductFormViewModel
    {
        public int Id { get; set; }

        [Required, MaxLength(200), Display(Name = "Name (English)")]
        public string NameEn { get; set; } = string.Empty;

        [Required, MaxLength(200), Display(Name = "Name (Arabic)")]
        public string NameAr { get; set; } = string.Empty;

        [MaxLength(500), Display(Name = "Description (English)")]
        public string? DescriptionEn { get; set; }

        [MaxLength(500), Display(Name = "Description (Arabic)")]
        public string? DescriptionAr { get; set; }

        [Required, Range(0.01, 10000), Display(Name = "Price (EGP)")]
        public decimal Price { get; set; }

        [Required, Display(Name = "Category")]
        public int CategoryId { get; set; }

        public bool IsAvailable { get; set; } = true;
        public bool IsFeatured { get; set; } = false;

        public string? ExistingImagePath { get; set; }

        [Display(Name = "Product Image")]
        public IFormFile? ImageFile { get; set; }

        public IEnumerable<SelectListItem> CategoryOptions { get; set; } = new List<SelectListItem>();
    }
}
