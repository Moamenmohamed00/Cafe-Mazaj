using Cafe_Mazaj.Data;
using Cafe_Mazaj.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Mazaj.Services.Gallery
{
    public class GalleryService : IGalleryService
    {
        private readonly AppDbContext _db;
        public GalleryService(AppDbContext db) => _db = db;

        public async Task<IEnumerable<GalleryImage>> GetAllAsync()
            => await _db.GalleryImages.OrderBy(g => g.SortOrder).ThenByDescending(g => g.UploadedAt).ToListAsync();

        public async Task<GalleryImage?> GetByIdAsync(int id)
            => await _db.GalleryImages.FindAsync(id);

        public async Task CreateAsync(GalleryImage image)
        {
            _db.GalleryImages.Add(image);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var img = await _db.GalleryImages.FindAsync(id);
            if (img != null)
            {
                _db.GalleryImages.Remove(img);
                await _db.SaveChangesAsync();
            }
        }
    }
}
