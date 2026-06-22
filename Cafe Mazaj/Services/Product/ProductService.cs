using Cafe_Mazaj.Data;
using Microsoft.EntityFrameworkCore;
using ProductEntity = Cafe_Mazaj.Models.Entities.Product;

namespace Cafe_Mazaj.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        public ProductService(AppDbContext db) => _db = db;

        public async Task<IEnumerable<ProductEntity>> GetAllAsync()
            => await _db.Products.Include(p => p.Category).OrderBy(p => p.CategoryId).ThenBy(p => p.NameEn).ToListAsync();

        public async Task<IEnumerable<ProductEntity>> GetByCategoryAsync(int categoryId)
            => await _db.Products.Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId && p.IsAvailable)
                .OrderBy(p => p.NameEn).ToListAsync();

        public async Task<IEnumerable<ProductEntity>> GetFeaturedAsync(int count = 6)
            => await _db.Products.Include(p => p.Category)
                .Where(p => p.IsFeatured && p.IsAvailable)
                .Take(count).ToListAsync();

        public async Task<ProductEntity?> GetByIdAsync(int id)
            => await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        public async Task CreateAsync(ProductEntity product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductEntity product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();
            }
        }
    }
}
