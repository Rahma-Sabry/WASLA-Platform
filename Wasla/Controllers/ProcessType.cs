using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wasla.Models;

namespace Wasla.Controllers
{
    public class ProcessTypesController : Controller
    {
        private readonly WaslaContext _context;

        public ProcessTypesController(WaslaContext context)
        {
            _context = context;
        }

        // GET: ProcessTypes
        public async Task<IActionResult> Index()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(HttpContext.Session.GetString("Email")));
            HttpContext.Session.SetString("UserId", user.Id.ToString()); HttpContext.Session.SetString("UserId", "1");
            return View(await _context.ProcessTypes.ToListAsync());
        }

        // GET: ProcessTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var pt = await _context.ProcessTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pt == null) return NotFound();

            return View(pt);
        }

        // GET: ProcessTypes/Create
        public IActionResult Create()
        {
            // log session for debug
            var uid = HttpContext.Session.GetString("UserId");
            Console.WriteLine("Current UserId session: " + uid);

            return View();
        }

        // POST: ProcessTypes/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProcessName")] ProcessType pt)
        {
            foreach (var kv in ModelState)
                foreach (var err in kv.Value.Errors)
                    Console.WriteLine($"Field {kv.Key}: {err.ErrorMessage}");

            if (ModelState.IsValid)
            {
                _context.Add(pt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("validation error");
            return View(pt);
        }

        // GET: ProcessTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pt = await _context.ProcessTypes.FindAsync(id);
            if (pt == null) return NotFound();

            return View(pt);
        }

        // POST: ProcessTypes/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProcessName")] ProcessType pt)
        {
            if (id != pt.Id) return NotFound();

            foreach (var kv in ModelState)
                foreach (var err in kv.Value.Errors)
                    Console.WriteLine($"Field {kv.Key}: {err.ErrorMessage}");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.ProcessTypes.Any(e => e.Id == pt.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pt);
        }

        // GET: ProcessTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var pt = await _context.ProcessTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pt == null) return NotFound();

            return View(pt);
        }

        // POST: ProcessTypes/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pt = await _context.ProcessTypes.FindAsync(id);
            if (pt != null)
            {
                _context.ProcessTypes.Remove(pt);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
