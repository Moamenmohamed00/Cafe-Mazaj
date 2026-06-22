using Cafe_Mazaj.Filters;
using Cafe_Mazaj.Models.Entities;
using Cafe_Mazaj.Services.Gallery;
using Cafe_Mazaj.Services.Images;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Mazaj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class GalleryController : Controller
    {
        private readonly IGalleryService _gallery;
        private readonly IImageService _images;

        public GalleryController(IGalleryService gallery, IImageService images)
        {
            _gallery = gallery;
            _images = images;
        }

        public async Task<IActionResult> Index()
            => View(await _gallery.GetAllAsync());

        [HttpGet]
        public IActionResult Upload() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile imageFile, string? altTextEn, string? altTextAr, string? tag)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                ModelState.AddModelError("", "Please select an image.");
                return View();
            }

            var path = await _images.SaveImageAsync(imageFile, "gallery");
            if (path == null)
            {
                ModelState.AddModelError("", "Invalid file type. Use JPG, PNG or WebP.");
                return View();
            }

            await _gallery.CreateAsync(new GalleryImage
            {
                ImagePath  = path,
                AltTextEn  = altTextEn,
                AltTextAr  = altTextAr,
                Tag        = tag
            });

            TempData["Success"] = "Image uploaded successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var img = await _gallery.GetByIdAsync(id);
            if (img != null)
            {
                _images.DeleteImage(img.ImagePath);
                await _gallery.DeleteAsync(id);
                TempData["Success"] = "Image deleted.";
            }
            return RedirectToAction("Index");
        }
    }
}
