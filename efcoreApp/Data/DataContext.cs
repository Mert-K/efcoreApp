using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Data
{
    public class DataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=efcoreApp;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        public DbSet<Kurs> Kurslar { get; set; }
        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<KursKayit> KursKayitlari { get; set; }
        public DbSet<Ogretmen> Ogretmenler { get; set; }
    }
}
