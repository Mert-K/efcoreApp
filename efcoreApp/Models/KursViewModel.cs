using efcoreApp.Data;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace efcoreApp.Models
{
    public class KursViewModel
    {
        public int KursId { get; set; }
        [Required(ErrorMessage ="Kurs Adı Gerekli Alandır")]
        [StringLength(50)]
        public string? Baslik { get; set; }
        public int OgretmenId { get; set; }
        public ICollection<KursKayit> KursKayitlari { get; set; } = new List<KursKayit>();

    }
}
