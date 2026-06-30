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
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Data Dummy Studio
            modelBuilder.Entity<Studio>().HasData(
                new Studio { Id = 1, NamaStudio = "Studio 1 Premiere", Kapasitas = 50, FasilitasTambahan = "Dolby Atmos, Recliner Seats" },
                new Studio { Id = 2, NamaStudio = "Studio 2 IMAX", Kapasitas = 150, FasilitasTambahan = "Layar IMAX 3D" },
                new Studio { Id = 3, NamaStudio = "Studio 3 Reguler", Kapasitas = 100, FasilitasTambahan = "AC, Kursi Standar" }
            );

            // 2. Data Dummy Film
            modelBuilder.Entity<Film>().HasData(
                new Film { Id = 1, JudulFilm = "Inception", Genre = "Sci-Fi", Durasi = 148, Deskripsi = "Pencuri yang mencuri rahasia lewat mimpi." },
                new Film { Id = 2, JudulFilm = "The Dark Knight", Genre = "Action", Durasi = 152, Deskripsi = "Batman melawan ancaman Joker di Gotham." },
                new Film { Id = 3, JudulFilm = "Interstellar", Genre = "Sci-Fi", Durasi = 169, Deskripsi = "Perjalanan ruang angkasa demi menyelamatkan umat manusia." }
            );

            // 3. Data Dummy JadwalFilm
            modelBuilder.Entity<JadwalFilm>().HasData(
                new JadwalFilm { Id = 1, StudioId = 1, FilmId = 1, WaktuTayang = new DateTime(2026, 7, 1, 14, 0, 0), HargaTiket = 75000 },
                new JadwalFilm { Id = 2, StudioId = 2, FilmId = 2, WaktuTayang = new DateTime(2026, 7, 1, 16, 30, 0), HargaTiket = 60000 },
                new JadwalFilm { Id = 3, StudioId = 3, FilmId = 3, WaktuTayang = new DateTime(2026, 7, 1, 19, 0, 0), HargaTiket = 50000 }
            );

            // 4. Data Dummy User 
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin", Email = "admin@cinema.com", PasswordHash = "admin123", Role = "Admin" },
                new User { Id = 2, Username = "user", Email = "user@cinema.com", PasswordHash = "user123", Role = "User" }
            );
        }
    }
}