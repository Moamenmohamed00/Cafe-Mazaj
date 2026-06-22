using Cafe_Mazaj.Models.Entities;

namespace Cafe_Mazaj.Services.Reservation
{
    public interface IReservationService
    {
        Task<IEnumerable<Models.Entities.Reservation>> GetAllAsync();
        Task<IEnumerable<Models.Entities.Reservation>> GetByStatusAsync(string status);
        Task<Models.Entities.Reservation?> GetByIdAsync(int id);
        Task CreateAsync(Models.Entities.Reservation reservation);
        Task UpdateStatusAsync(int id, string status);
    }
}
