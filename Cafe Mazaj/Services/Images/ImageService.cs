using Cafe_Mazaj.Services.Images;

namespace Cafe_Mazaj.Services.Images
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _env;

        public ImageService(IWebHostEnvironment env) => _env = env;

        public async Task<string?> SaveImageAsync(IFormFile file, string folder)
        {
            if (file == null || file.Length == 0) return null;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(ext)) return null;

            var uploadPath = Path.Combine(_env.WebRootPath, "uploads", folder);
            Directory.CreateDirectory(uploadPath);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(uploadPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/{folder}/{fileName}";
        }

        public void DeleteImage(string? imagePath)
        {
            if (string.IsNullOrWhiteSpace(imagePath)) return;
            // Skip seed/placeholder images
            if (!imagePath.StartsWith("/uploads/")) return;

            var fullPath = Path.Combine(_env.WebRootPath, imagePath.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}
