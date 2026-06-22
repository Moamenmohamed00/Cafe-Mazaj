using Cafe_Mazaj.Data;
using Cafe_Mazaj.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Mazaj.Services.Reservation
{
    public class ReservationService : IReservationService
    {
        private readonly AppDbContext _db;
        public ReservationService(AppDbContext db) => _db = db;

        public async Task<IEnumerable<Models.Entities.Reservation>> GetAllAsync()
            => await _db.Reservations.OrderByDescending(r => r.CreatedAt).ToListAsync();

        public async Task<IEnumerable<Models.Entities.Reservation>> GetByStatusAsync(string status)
            => await _db.Reservations.Where(r => r.Status == status).OrderByDescending(r => r.CreatedAt).ToListAsync();

        public async Task<Models.Entities.Reservation?> GetByIdAsync(int id)
            => await _db.Reservations.FindAsync(id);

        public async Task CreateAsync(Models.Entities.Reservation reservation)
        {
            _db.Reservations.Add(reservation);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            var r = await _db.Reservations.FindAsync(id);
            if (r != null)
            {
                r.Status = status;
                await _db.SaveChangesAsync();
            }
        }
    }
}
