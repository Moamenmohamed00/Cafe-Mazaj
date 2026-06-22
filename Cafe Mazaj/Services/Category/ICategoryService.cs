using Cafe_Mazaj.Models.Entities;

namespace Cafe_Mazaj.Services.Category
{
    public interface ICategoryService
    {
        Task<IEnumerable<Models.Entities.Category>> GetAllAsync();
        Task<Models.Entities.Category?> GetByIdAsync(int id);
        Task CreateAsync(Models.Entities.Category category);
        Task UpdateAsync(Models.Entities.Category category);
        Task DeleteAsync(int id);
    }
}
