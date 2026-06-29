namespace CinemaAPI.Models
{
    public class Tiket
    {
        public int Id { get; set; }

        public int JadwalFilmId { get; set; }
        public JadwalFilm? JadwalFilm { get; set; }

        public string UserId { get; set; } = string.Empty;        
        public string NomorKursi { get; set; } = string.Empty;

        public string StatusTiket { get; set; } = "Terkonfirmasi";
    }
}