using Cafe_Mazaj.Filters;
using Cafe_Mazaj.Models.Entities;
using Cafe_Mazaj.Models.ViewModels.Admin;
using Cafe_Mazaj.Services.Category;
using Cafe_Mazaj.Services.Images;
using Cafe_Mazaj.Services.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cafe_Mazaj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class ProductController : Controller
    {
        private readonly IProductService _products;
        private readonly ICategoryService _categories;
        private readonly IImageService _images;

        public ProductController(IProductService products, ICategoryService categories, IImageService images)
        {
            _products = products;
            _categories = categories;
            _images = images;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _products.GetAllAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new ProductFormViewModel
            {
                CategoryOptions = await GetCategoryOptions()
            };
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.CategoryOptions = await GetCategoryOptions();
                return View(vm);
            }

            var imagePath = await _images.SaveImageAsync(vm.ImageFile!, "products");

            var product = new Models.Entities.Product
            {
                NameEn        = vm.NameEn,
                NameAr        = vm.NameAr,
                DescriptionEn = vm.DescriptionEn,
                DescriptionAr = vm.DescriptionAr,
                Price         = vm.Price,
                CategoryId    = vm.CategoryId,
                IsAvailable   = vm.IsAvailable,
                IsFeatured    = vm.IsFeatured,
                ImagePath     = imagePath
            };

            await _products.CreateAsync(product);
            TempData["Success"] = "Product created successfully.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _products.GetByIdAsync(id);
            if (product == null) return NotFound();

            var vm = new ProductFormViewModel
            {
                Id                = product.Id,
                NameEn            = product.NameEn,
                NameAr            = product.NameAr,
                DescriptionEn     = product.DescriptionEn,
                DescriptionAr     = product.DescriptionAr,
                Price             = product.Price,
                CategoryId        = product.CategoryId,
                IsAvailable       = product.IsAvailable,
                IsFeatured        = product.IsFeatured,
                ExistingImagePath = product.ImagePath,
                CategoryOptions   = await GetCategoryOptions()
            };
            return View(vm);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.CategoryOptions = await GetCategoryOptions();
                return View(vm);
            }

            var product = await _products.GetByIdAsync(id);
            if (product == null) return NotFound();

            if (vm.ImageFile != null && vm.ImageFile.Length > 0)
            {
                _images.DeleteImage(product.ImagePath);
                product.ImagePath = await _images.SaveImageAsync(vm.ImageFile, "products");
            }

            product.NameEn        = vm.NameEn;
            product.NameAr        = vm.NameAr;
            product.DescriptionEn = vm.DescriptionEn;
            product.DescriptionAr = vm.DescriptionAr;
            product.Price         = vm.Price;
            product.CategoryId    = vm.CategoryId;
            product.IsAvailable   = vm.IsAvailable;
            product.IsFeatured    = vm.IsFeatured;

            await _products.UpdateAsync(product);
            TempData["Success"] = "Product updated successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _products.GetByIdAsync(id);
            if (product != null)
            {
                _images.DeleteImage(product.ImagePath);
                await _products.DeleteAsync(id);
                TempData["Success"] = "Product deleted.";
            }
            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> GetCategoryOptions()
        {
            var cats = await _categories.GetAllAsync();
            return cats.Select(c => new SelectListItem(c.NameEn, c.Id.ToString()));
        }
    }
}
