using efcoreApp.Data;

namespace efcoreApp.Models
{
    public class KursKayitDTO
    {
        public int KayitId { get; set; }
        public int OgrenciId { get; set; }
        public int KursId { get; set; }
        public Kurs? Kurs { get; set; } = null!; //navigation property
        public Ogrenci? Ogrenci { get; set; } = null!;
        public DateTime KayitTarihi { get; set; }
    }
}
