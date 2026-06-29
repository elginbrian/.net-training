namespace CinemaAPI.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string JudulFilm { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int Durasi { get; set; }
        public string Deskripsi { get; set;} = string.Empty;
    }
}