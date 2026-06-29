namespace CinemaAPI.Models
{
    public class Transaksi
    {
        public int Id { get; set; }

        public int TiketId { get; set; }
        public Tiket? Tiket { get; set; }

        public string MetodePembayaran { get; set; } = string.Empty;
        public string StatusPembayaran { get; set; } = "Pending";
    }
}