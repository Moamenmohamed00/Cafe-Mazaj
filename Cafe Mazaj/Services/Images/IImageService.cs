namespace Cafe_Mazaj.Services.Images
{
    public interface IImageService
    {
        Task<string?> SaveImageAsync(IFormFile file, string folder);
        void DeleteImage(string? imagePath);
    }
}
