using ProductEntity = Cafe_Mazaj.Models.Entities.Product;

namespace Cafe_Mazaj.Services.Product
{
    public interface IProductService
    {
        Task<IEnumerable<ProductEntity>> GetAllAsync();
        Task<IEnumerable<ProductEntity>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<ProductEntity>> GetFeaturedAsync(int count = 6);
        Task<ProductEntity?> GetByIdAsync(int id);
        Task CreateAsync(ProductEntity product);
        Task UpdateAsync(ProductEntity product);
        Task DeleteAsync(int id);
    }
}
