using efcoreApp.Data;
using efcoreApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace efcoreApp.Controllers
{
    public class KursController : Controller
    {
        private readonly DataContext _context;

        public KursController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kurslar.Include(x => x.Ogretmen).ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.selectListItems = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(KursViewModel kursViewModel)
        {
            if(ModelState.IsValid)
            {
                _context.Kurslar.Add(new Kurs()
                {
                    KursId = kursViewModel.KursId,
                    Baslik = kursViewModel.Baslik,
                    OgretmenId = kursViewModel.OgretmenId,
                    KursKayitlari = kursViewModel.KursKayitlari
                });
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.selectListItems = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View(kursViewModel);
            
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            KursViewModel? kurs = await _context.Kurslar
                .Include(x => x.KursKayitlari)
                .ThenInclude(x => x.Ogrenci)
                .Select(x=>new KursViewModel
                {
                    KursId = x.KursId,
                    Baslik = x.Baslik,
                    OgretmenId = x.OgretmenId,
                    KursKayitlari = x.KursKayitlari
                })
                .FirstOrDefaultAsync(x => x.KursId == id);

            if (kurs == null)
            {
                return NotFound();
            }
            ViewBag.selectListItems = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View(kurs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, KursViewModel kursViewModel)
        {
            if (id != kursViewModel.KursId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Kurslar.Update(new Kurs()
                    {
                        KursId = kursViewModel.KursId,
                        Baslik = kursViewModel.Baslik,
                        OgretmenId= kursViewModel.OgretmenId,
                        KursKayitlari = kursViewModel.KursKayitlari
                    });
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (!_context.Kurslar.Any(k => k.KursId == kursViewModel.KursId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.selectListItems = new SelectList(await _context.Ogretmenler.ToListAsync(), "OgretmenId", "AdSoyad");
            return View(kursViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Kurs? kurs = await _context.Kurslar.FindAsync(id);

            if (kurs == null)
            {
                return NotFound();
            }

            return View(kurs);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] int id) //Form'dan gelen id , route'dan değil.
        {
            Kurs? kurs = await _context.Kurslar.FindAsync(id);

            if (kurs == null)
            {
                return NotFound();
            }

            _context.Kurslar.Remove(kurs);
            await _context.SaveChangesAsync();
            TempData["mesaj"] = "Kurs Silindi";
            return RedirectToAction("Index");
        }
    }
}
