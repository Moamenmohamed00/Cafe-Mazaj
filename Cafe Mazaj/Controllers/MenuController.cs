using Cafe_Mazaj.Models.ViewModels;
using Cafe_Mazaj.Services.Category;
using Cafe_Mazaj.Services.Product;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Mazaj.Controllers
{
    public class MenuController : Controller
    {
        private readonly IProductService _products;
        private readonly ICategoryService _categories;

        public MenuController(IProductService products, ICategoryService categories)
        {
            _products = products;
            _categories = categories;
        }

        public async Task<IActionResult> Index(int? categoryId)
        {
            var categories = await _categories.GetAllAsync();
            var products = categoryId.HasValue
                ? await _products.GetByCategoryAsync(categoryId.Value)
                : await _products.GetAllAsync();

            var vm = new MenuViewModel
            {
                Categories = categories,
                Products = products.Where(p => p.IsAvailable),
                SelectedCategoryId = categoryId
            };
            return View(vm);
        }
    }
}
