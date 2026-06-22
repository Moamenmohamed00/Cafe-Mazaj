using Cafe_Mazaj.Data;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Mazaj.Services.ContactMessage
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _db;
        public ContactService(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Models.Entities.ContactMessage>> GetAllAsync()
            => await _db.ContactMessages.OrderByDescending(m => m.ReceivedAt).ToListAsync();

        public async Task<Models.Entities.ContactMessage?> GetByIdAsync(int id)
            => await _db.ContactMessages.FindAsync(id);

        public async Task CreateAsync(Models.Entities.ContactMessage message)
        {
            _db.ContactMessages.Add(message);
            await _db.SaveChangesAsync();
        }

        public async Task MarkAsReadAsync(int id)
        {
            var msg = await _db.ContactMessages.FindAsync(id);
            if (msg != null)
            {
                msg.IsRead = true;
                await _db.SaveChangesAsync();
            }
        }

        public async Task<int> GetUnreadCountAsync()
            => await _db.ContactMessages.CountAsync(m => !m.IsRead);
    }
}
