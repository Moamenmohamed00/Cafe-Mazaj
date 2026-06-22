using Cafe_Mazaj.Data;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Mazaj.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _db;
        public CategoryService(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Models.Entities.Category>> GetAllAsync()
            => await _db.Categories.OrderBy(c => c.SortOrder).ToListAsync();

        public async Task<Models.Entities.Category?> GetByIdAsync(int id)
            => await _db.Categories.FindAsync(id);

        public async Task CreateAsync(Models.Entities.Category category)
        {
            _db.Categories.Add(category);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Models.Entities.Category category)
        {
            _db.Categories.Update(category);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var cat = await _db.Categories.FindAsync(id);
            if (cat != null)
            {
                _db.Categories.Remove(cat);
                await _db.SaveChangesAsync();
            }
        }
    }
}
