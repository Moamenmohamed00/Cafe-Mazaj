using Cafe_Mazaj.Models.ViewModels;
using Cafe_Mazaj.Services.Product;
using Cafe_Mazaj.Services.Gallery;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Cafe_Mazaj.Models;

namespace Cafe_Mazaj.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _products;
        private readonly IGalleryService _gallery;

        public HomeController(IProductService products, IGalleryService gallery)
        {
            _products = products;
            _gallery = gallery;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new HomeViewModel
            {
                FeaturedProducts = await _products.GetFeaturedAsync(6),
                GalleryPreview   = await _gallery.GetPreviewAsync(6)  // SQL-level limit, no waste
            };
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
