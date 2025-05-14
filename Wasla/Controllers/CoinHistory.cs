using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wasla.Models;

namespace Wasla.Controllers
{
    public class CoinHistoriesController : Controller
    {
        private readonly WaslaContext _context;

        public CoinHistoriesController(WaslaContext context)
        {
            _context = context;
        }

        // GET: CoinHistories
        public async Task<IActionResult> Index()
        {
            var coinHistories = _context.CoinHistories
                .Include(c => c.ProcessType)
                .Include(c => c.User);
            return View(await coinHistories.ToListAsync());
        }

        // GET: CoinHistories/Details/5?time=2024-01-01T12:00:00
        public async Task<IActionResult> Details(int? id, DateTime? time)
        {
            if (id == null || time == null)
            {
                return NotFound();
            }

            var coinHistory = await _context.CoinHistories
                .Include(c => c.ProcessType)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.UserId == id && m.Time == time);

            if (coinHistory == null)
            {
                return NotFound();
            }

            return View(coinHistory);
        }

        // GET: CoinHistories/Delete/5?time=2024-01-01T12:00:00
        public async Task<IActionResult> Delete(int? id, DateTime? time)
        {
            if (id == null || time == null)
            {
                return NotFound();
            }

            var coinHistory = await _context.CoinHistories
                .Include(c => c.ProcessType)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.UserId == id && m.Time == time);

            if (coinHistory == null)
            {
                return NotFound();
            }

            return View(coinHistory);
        }

        // POST: CoinHistories/Delete/5?time=2024-01-01T12:00:00
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, DateTime time)
        {
            var coinHistory = await _context.CoinHistories.FindAsync(id, time);
            if (coinHistory != null)
            {
                _context.CoinHistories.Remove(coinHistory);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CoinHistoryExists(int id, DateTime time)
        {
            return _context.CoinHistories.Any(e => e.UserId == id && e.Time == time);
        }
    }
}
