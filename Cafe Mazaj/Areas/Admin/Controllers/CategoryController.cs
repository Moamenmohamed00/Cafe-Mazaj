using Cafe_Mazaj.Filters;
using Cafe_Mazaj.Models.Entities;
using Cafe_Mazaj.Services.Category;
using Microsoft.AspNetCore.Mvc;

namespace Cafe_Mazaj.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categories;
        public CategoryController(ICategoryService categories) => _categories = categories;

        public async Task<IActionResult> Index()
            => View(await _categories.GetAllAsync());

        [HttpGet]
        public IActionResult Create() => View(new Models.Entities.Category());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Entities.Category category)
        {
            if (!ModelState.IsValid) return View(category);
            // Auto-generate slug
            category.Slug = category.NameEn.ToLower().Replace(" ", "-");
            await _categories.CreateAsync(category);
            TempData["Success"] = "Category created.";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var cat = await _categories.GetByIdAsync(id);
            if (cat == null) return NotFound();
            return View(cat);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Models.Entities.Category category)
        {
            if (!ModelState.IsValid) return View(category);
            category.Slug = category.NameEn.ToLower().Replace(" ", "-");
            await _categories.UpdateAsync(category);
            TempData["Success"] = "Category updated.";
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _categories.DeleteAsync(id);
            TempData["Success"] = "Category deleted.";
            return RedirectToAction("Index");
        }
    }
}
