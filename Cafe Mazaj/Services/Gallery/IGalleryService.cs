using Cafe_Mazaj.Models.Entities;

namespace Cafe_Mazaj.Services.Gallery
{
    public interface IGalleryService
    {
        Task<IEnumerable<GalleryImage>> GetAllAsync();
        Task<IEnumerable<GalleryImage>> GetPreviewAsync(int count = 6); // DB-level limit
        Task<GalleryImage?> GetByIdAsync(int id);
        Task CreateAsync(GalleryImage image);
        Task DeleteAsync(int id);
    }
}
