using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wasla.Models;

namespace WaslaHiringPlatform.Controllers
{
    public class UsersController : Controller
    {
        private readonly WaslaContext _context;

        public UsersController(WaslaContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            Console.WriteLine("======================================================================");
            var users = await _context.Users.ToListAsync();
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Optional: You can log validation errors for debugging
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return View(user);
        }


        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();
            //user.ValidatePassword = user.Password;

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Users.Any(u => u.Id.Equals(user.Id)))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(user);
        }


        // GIT: Users/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recruiter = await _context.Recruiters
                .FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (recruiter == null)
            {
                return NotFound();
            }

            return View(recruiter);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.Id.Equals(id));
        }
    }
}
