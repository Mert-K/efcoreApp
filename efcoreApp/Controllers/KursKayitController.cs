using Microsoft.AspNetCore.Mvc;
using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using efcoreApp.Models;

namespace efcoreApp.Controllers
{
    public class KursKayitController : Controller
    {
        private readonly DataContext _context;
        public KursKayitController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var kursKayitlari = await _context
                                .KursKayitlari
                                .Include(x => x.Ogrenci) //KursKayit class'ında bulunan Ogrenci isimli navigation propert'i ekleme
                                .Include(x => x.Kurs)    //KursKayit class'ında bulunan Kurs isimli navigation propert'i ekleme
                                .ToListAsync();
            return View(kursKayitlari);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursKayitDTO kursKayitDTO)
        {
            if (ModelState.IsValid)
            {
                _context.KursKayitlari.Add(new KursKayit()
                {
                    OgrenciId = kursKayitDTO.OgrenciId,
                    KursId = kursKayitDTO.KursId,
                    KayitTarihi = DateTime.Now
                });
                await _context.SaveChangesAsync();
                TempData["mesaj"] = "Kurs Kaydı Eklendi";
                return RedirectToAction("Index");
            }
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");
            return View(kursKayitDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            KursKayit? kursKayit = await _context.KursKayitlari.FindAsync(id);

            if (kursKayit == null)
            {
                return NotFound();
            }
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");
            return View(new KursKayitDTO() { KayitId = kursKayit.KayitId, KursId = kursKayit.KursId, OgrenciId = kursKayit.OgrenciId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, KursKayitDTO kursKayitDTO)
        {
            if (id != kursKayitDTO.KayitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                KursKayit? kursKayit = await _context.KursKayitlari.FirstOrDefaultAsync(x => x.KayitId == kursKayitDTO.KayitId);

                kursKayit!.KayitId = kursKayitDTO.KayitId;
                kursKayit!.OgrenciId = kursKayitDTO.OgrenciId;
                kursKayit!.KursId = kursKayitDTO.KursId;

                _context.KursKayitlari.Update(kursKayit!);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Ogrenciler = new SelectList(await _context.Ogrenciler.ToListAsync(), "OgrenciId", "AdSoyad");
            ViewBag.Kurslar = new SelectList(await _context.Kurslar.ToListAsync(), "KursId", "Baslik");
            return View(kursKayitDTO);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            KursKayit? kursKayit = _context.KursKayitlari
                                            .Include(x => x.Ogrenci)
                                            .Include(x => x.Kurs)
                                            .FirstOrDefault(x=>x.KayitId==id);

            if (kursKayit == null)
            {
                return NotFound();
            }
            return View(new KursKayitDTO()
            {
                KayitId = kursKayit.KayitId,
                KursId = kursKayit.KursId,
                OgrenciId = kursKayit.OgrenciId,
                Kurs=kursKayit.Kurs,
                Ogrenci = kursKayit.Ogrenci,
                KayitTarihi = kursKayit.KayitTarihi
            });
        }

        [HttpPost]
        public IActionResult Delete(int? id,KursKayitDTO kursKayitDTO)
        {
            if(id!=kursKayitDTO.KayitId)
            {
                return NotFound();
            }
            KursKayit? kursKayit = _context.KursKayitlari.Find(id);
            _context.KursKayitlari.Remove(kursKayit!);
            _context.SaveChanges();
            TempData["mesaj"] = "Kurs Kaydı Silindi";
            return RedirectToAction("Index");
        }
    }
}
