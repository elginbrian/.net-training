using CinemaAPI.Data;
using CinemaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Services
{
    public class TiketService : ITiketService
    {
        private readonly AppDbContext _context;

        public TiketService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Tiket> PesanTiketAsync(Tiket tiket, string userEmail)
        {
            bool isSeatTaken = await _context.Tikets.AnyAsync(t => 
                t.JadwalFilmId == tiket.JadwalFilmId && 
                t.NomorKursi == tiket.NomorKursi && 
                t.StatusTiket != "Dibatalkan");

            if (isSeatTaken)
            {
                throw new InvalidOperationException($"Maaf, kursi {tiket.NomorKursi} sudah dipesan untuk jadwal ini.");
            }

            if (!string.IsNullOrEmpty(userEmail))
            {
                tiket.UserId = userEmail;
            }

            _context.Tikets.Add(tiket);
            await _context.SaveChangesAsync();

            var createdTiket = await _context.Tikets
                .Include(t => t.JadwalFilm)
                .ThenInclude(j => j!.Film)
                .Include(t => t.JadwalFilm)
                .ThenInclude(j => j!.Studio)
                .FirstOrDefaultAsync(t => t.Id == tiket.Id);

            return createdTiket!;
        }
    }
}
