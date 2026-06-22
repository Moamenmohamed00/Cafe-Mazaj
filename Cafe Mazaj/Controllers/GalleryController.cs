using Cafe_Mazaj.Services.Gallery;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Mazaj.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGalleryService _gallery;
        public GalleryController(IGalleryService gallery) => _gallery = gallery;

        public async Task<IActionResult> Index()
        {
            var images = await _gallery.GetAllAsync();
            return View(images);
        }
    }
}
