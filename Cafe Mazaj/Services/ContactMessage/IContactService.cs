using Cafe_Mazaj.Models.Entities;

namespace Cafe_Mazaj.Services.ContactMessage
{
    public interface IContactService
    {
        Task<IEnumerable<Models.Entities.ContactMessage>> GetAllAsync();
        Task<Models.Entities.ContactMessage?> GetByIdAsync(int id);
        Task CreateAsync(Models.Entities.ContactMessage message);
        Task MarkAsReadAsync(int id);
        Task<int> GetUnreadCountAsync();
    }
}
