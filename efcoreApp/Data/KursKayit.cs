using efcoreApp.Data;
using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class KursKayit
    {
        [Key]
        public int KayitId { get; set; }
        public int OgrenciId { get; set; }
        public Ogrenci Ogrenci { get; set; } = null!; //navigation property (Foreign Key Convention'ı şu şekilde oluşturur: Tipi öğrenci olan property isminin(=Ogrenci) yanına ID koyup class içinde OgrenciID diye property arar. Var ise onu Foreign Key yapar.)
        public int KursId { get; set; }
        public Kurs Kurs { get; set; } = null!; //navigation property
        public DateTime KayitTarihi { get; set; }
    }
}



