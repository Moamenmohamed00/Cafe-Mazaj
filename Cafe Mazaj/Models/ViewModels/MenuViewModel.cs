using Cafe_Mazaj.Models.Entities;

namespace Cafe_Mazaj.Models.ViewModels
{
    public class MenuViewModel
    {
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public int? SelectedCategoryId { get; set; }
    }
}
