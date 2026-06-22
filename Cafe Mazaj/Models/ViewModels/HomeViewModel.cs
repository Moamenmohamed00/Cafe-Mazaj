using Cafe_Mazaj.Models.Entities;

namespace Cafe_Mazaj.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> FeaturedProducts { get; set; } = new List<Product>();
        public IEnumerable<GalleryImage> GalleryPreview { get; set; } = new List<GalleryImage>();
    }
}
