using System;
using System.Linq;
using System.Threading.Tasks;
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
            // session mimic
            HttpContext.Session.SetString("UserId", "1");

            var list = _context.CoinHistories
                .Include(ch => ch.User)
                .Include(ch => ch.ProcessType);
            return View(await list.ToListAsync());
        }

        // GET: CoinHistories/Details?userId=1&time=2025-05-13T14:30:00
        public async Task<IActionResult> Details(int? userId, DateTime? time)
        {
            if (userId == null || time == null)
                return NotFound();

            var ch = await _context.CoinHistories
                .Include(x => x.User)
                .Include(x => x.ProcessType)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Time == time.Value);

            if (ch == null)
                return NotFound();

            return View(ch);
        }

        // GET: CoinHistories/Create
        // GET: CoinHistories/Create
        public IActionResult Create()
        {
            // 1) Seed a demo User if none exists
            if (!_context.Users.Any())
            {
                _context.Users.Add(new User
                {
                    FirstName = "Demo",
                    LastName = "User",
                    Email = "demo@wasla.local",
                    SSN = "111-11-1111",
                    Phone = "0123456789",
                    Password = "Password123!",
                    ValidatePassword = "Password123!",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Coins = 100
                });
            }

            // 2) Seed two demo ProcessTypes if none exist
            if (!_context.ProcessTypes.Any())
            {
                _context.ProcessTypes.AddRange(
                    new ProcessType
                    {
                        // adjust this property name if yours differs
                        ProcessName = "Credit"
                    },
                    new ProcessType
                    {
                        ProcessName = "Debit"
                    }
                );
            }

            // 3) Save seeds
            _context.SaveChanges();

            // 4) Pull them back into lists (ToList() ensures non-null)
            var users = _context.Users.ToList();
            var pts = _context.ProcessTypes.ToList();

            // 5) Populate your dropdowns
            ViewData["UserId"] = new SelectList(users, "UserId", "FirstName");
            ViewData["ProcessTypeId"] = new SelectList(pts, "ProcessTypeId", "ProcessTypeName");
            return View();
        }


        
        // POST: CoinHistories/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,Time,ProcessTypeId,Amount")] CoinHistory ch)
        {
            // 1) Log every ModelState error
            foreach (var kv in ModelState)
                foreach (var err in kv.Value.Errors)
                    Console.WriteLine($"Field {kv.Key}: {err.ErrorMessage}");

            // 2) If valid, add & save
            if (ModelState.IsValid)
            {
                _context.CoinHistories.Add(ch);
                await _context.SaveChangesAsync();
                Console.WriteLine("[POST] CoinHistory saved.");
                return RedirectToAction(nameof(Index));
            }

            // 3) Otherwise, log and repopulate dropdowns for the redisplayed form
            Console.WriteLine("[POST] Validation failed—redisplaying form.");

            // **IMPORTANT**: re-fetch lists so ViewData isn’t null
            var users = _context.Users.ToList();
            var pts = _context.ProcessTypes.ToList();

            ViewData["UserId"] = new SelectList(users, "UserId", "FirstName", ch.UserId);
            ViewData["ProcessTypeId"] = new SelectList(pts, "ProcessTypeId", "ProcessTypeName", ch.ProcessTypeId);

            return View(ch);
        }


        // GET: CoinHistories/Edit?userId=1&time=2025-05-13T14:30:00
        public async Task<IActionResult> Edit(int? userId, DateTime? time)
        {
            if (userId == null || time == null)
                return NotFound();

            var ch = await _context.CoinHistories.FindAsync(userId, time.Value);
            if (ch == null)
                return NotFound();

            ViewData["UserId"]        = new SelectList(_context.Users,        "UserId", "FirstName",      ch.UserId);
            ViewData["ProcessTypeId"] = new SelectList(_context.ProcessTypes,"ProcessTypeId", "TypeName", ch.ProcessTypeId);
            return View(ch);
        }

        // POST: CoinHistories/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int userId, DateTime time, [Bind("UserId,Time,ProcessTypeId,Amount")] CoinHistory ch)
        {
            if (userId != ch.UserId || time != ch.Time)
                return NotFound();

            foreach (var kv in ModelState)
                foreach (var err in kv.Value.Errors)
                    Console.WriteLine($"Field {kv.Key}: {err.ErrorMessage}");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.CoinHistories.Any(x => x.UserId == userId && x.Time == time))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["UserId"]        = new SelectList(_context.Users,        "UserId", "FirstName",      ch.UserId);
            ViewData["ProcessTypeId"] = new SelectList(_context.ProcessTypes,"ProcessTypeId", "TypeName", ch.ProcessTypeId);
            return View(ch);
        }

        // GET: CoinHistories/Delete?userId=1&time=2025-05-13T14:30:00
        public async Task<IActionResult> Delete(int? userId, DateTime? time)
        {
            if (userId == null || time == null)
                return NotFound();

            var ch = await _context.CoinHistories
                .Include(x => x.User)
                .Include(x => x.ProcessType)
                .FirstOrDefaultAsync(x => x.UserId == userId && x.Time == time.Value);

            if (ch == null)
                return NotFound();

            return View(ch);
        }

        // POST: CoinHistories/Delete
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int userId, DateTime time)
        {
            var ch = await _context.CoinHistories.FindAsync(userId, time);
            if (ch != null)
            {
                _context.CoinHistories.Remove(ch);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
