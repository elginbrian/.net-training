using CinemaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Studio> Studios { get; set;}
        public DbSet<Film> Films { get; set; }
        public DbSet<JadwalFilm> JadwalFilms { get; set; }
        public DbSet<Tiket> Tikets { get; set; }
        public DbSet<Transaksi> Transaksis { get; set; }
        
    }
}