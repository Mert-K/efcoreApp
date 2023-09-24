using efcoreApp.Data;
using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Ogrenci
    {
        [Key]
        public int OgrenciId { get; set; }
        [Required(ErrorMessage ="Öğrenci Adı Zorunlu Alandır,Lütfen Doldurunuz")]
        public string? OgrenciAd { get; set; }
        [Required(ErrorMessage = "Öğrenci Soyadı Zorunlu Alandır,Lütfen Doldurunuz")]
        public string? OgrenciSoyad { get; set; }

        public string? AdSoyad
        {
            get
            {
                return this.OgrenciAd + " " + this.OgrenciSoyad;
            }
        }

        public string? Eposta { get; set; }
        public string? Telefon { get; set; }
        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();
        //Navigation property //null uyarısı vermesin diye new'lendi. null! diye de yazabilirdik.
    }
}



