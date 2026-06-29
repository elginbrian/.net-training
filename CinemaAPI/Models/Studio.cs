namespace CinemaAPI.Models
{
    public class Studio
    {
        public int Id { get; set;}
        public string NamaStudio { get; set; } = string.Empty;
        public int Kapasitas { get; set;}
        public string FasilitasTambahan { get; set; } = string.Empty;
    }
}