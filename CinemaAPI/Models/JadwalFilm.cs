namespace CinemaAPI.Models
{
    public class JadwalFilm
    {
        public int Id { get; set; }
        public int StudioId { get; set; }
        public Studio? Studio { get; set; }
        public int FilmId { get; set; }
        public Film? Film { get; set; }

        public DateTime WaktuTayang { get; set; }
        public decimal HargaTiket { get; set; }
    }
}