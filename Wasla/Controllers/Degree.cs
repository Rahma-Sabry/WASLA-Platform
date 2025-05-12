using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wasla.Models;

namespace Wasla.Controllers
{
    public class DegreeController : Controller
    {
        private readonly WaslaContext _context;

        public DegreeController(WaslaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.DegreeTypes.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Degree degree)
        {
            if (ModelState.IsValid)
            {
                _context.Add(degree);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(degree);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var degree = await _context.DegreeTypes.FindAsync(id);
            if (degree == null) return NotFound();

            return View(degree);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Degree degree)
        {
            if (id != degree.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(degree);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DegreeExists(degree.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(degree);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var degree = await _context.DegreeTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (degree == null) return NotFound();

            return View(degree);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var degree = await _context.DegreeTypes.FindAsync(id);
            if (degree != null)
            {
                _context.DegreeTypes.Remove(degree);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool DegreeExists(int id)
        {
            return _context.DegreeTypes.Any(e => e.Id == id);
        }
    }
}
