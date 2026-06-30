using CinemaAPI.Models;

namespace CinemaAPI.Services
{
    public interface ITiketService
    {
        Task<Tiket> PesanTiketAsync(Tiket tiket, string userEmail);
    }
}
