using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Data
{
    public class Ogretmen
    {
        [Key]
        public int OgretmenId { get; set; }
        [Required(ErrorMessage ="Öğretmen Adı Girişi Zorunludur")]
        public string? Ad { get; set; }
        [Required(ErrorMessage = "Öğretmen Soyadı Girişi Zorunludur")]
        public string? Soyad { get; set; }
        public string? Eposta { get; set; }
        [Required(ErrorMessage = "Öğretmen Telefon Girişi Zorunludur")]
        public string? Telefon { get; set; }

        public string? AdSoyad
        {
            get
            {
                return this.Ad + " " + this.Soyad;
            }
        }

        [DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BaslamaTarihi { get; set; }

        public ICollection<Kurs> Kurslar { get; set; } = new List<Kurs>();
    }
}
