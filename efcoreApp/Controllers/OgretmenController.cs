using efcoreApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace efcoreApp.Controllers
{
    public class OgretmenController : Controller
    {
        private readonly DataContext _context;

        public OgretmenController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Ogretmenler.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ogretmen model)
        {
            if (ModelState.IsValid)
            {
                _context.Ogretmenler.Add(model);
                await _context.SaveChangesAsync();
                TempData["mesaj"] = "Öğretmen Eklendi";
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ogretmen? entity = await _context
                                     .Ogretmenler
                                     .Include(x => x.Kurslar)
                                     .FirstOrDefaultAsync(o => o.OgretmenId == id);
            if (entity == null)
            {
                return NotFound();
            }

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ogretmen model)
        {
            if (id != model.OgretmenId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                    TempData["mesaj"] = "Öğretmen Bilgileri Güncellendi";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Ogretmenler.Any(o => o.OgretmenId == model.OgretmenId))
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
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Ogretmen? ogretmen = _context.Ogretmenler.Find(id);

            if (ogretmen == null)
            {
                return NotFound();
            }

            return View(ogretmen);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id, int? modelid)
        {
            if (id != modelid)
            {
                return NotFound();
            }

            Ogretmen? ogretmen = _context.Ogretmenler.Find(id);

            if (ogretmen == null)
            {
                return NotFound();
            }

            _context.Ogretmenler.Remove(ogretmen);
            _context.SaveChanges();
            TempData["mesaj"] = "Öğretmen Kaydı Silindi";
            return RedirectToAction("Index");
        }
    }
}
