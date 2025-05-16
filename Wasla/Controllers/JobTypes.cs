using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wasla.Models;

namespace Wasla.Controllers
{
    public class JobTypesController : Controller
    {
        private readonly WaslaContext _context;

        public JobTypesController(WaslaContext context)
        {
            _context = context;
        }

        // GET: JobTypes
        public async Task<IActionResult> Index()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(HttpContext.Session.GetString("Email")));
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            return View(await _context.JobTypes.ToListAsync());
        }

        // GET: JobTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var jobType = await _context.JobTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (jobType == null) return NotFound();
            return View(jobType);
        }

        // GET: JobTypes/Create
        public IActionResult Create()
        {
            Console.WriteLine("Session UserId = " + HttpContext.Session.GetString("UserId"));
            return View();
        }

        // POST: JobTypes/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TypeName")] JobType jobType)
        {
            foreach (var kv in ModelState)
                foreach (var err in kv.Value.Errors)
                    Console.WriteLine($"Field {kv.Key}: {err.ErrorMessage}");

            if (ModelState.IsValid)
            {
                _context.Add(jobType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine("validation error");
            return View(jobType);
        }

        // GET: JobTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var jobType = await _context.JobTypes.FindAsync(id);
            if (jobType == null) return NotFound();
            return View(jobType);
        }

        // POST: JobTypes/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeName")] JobType jobType)
        {
            if (id != jobType.Id) return NotFound();
            foreach (var kv in ModelState)
                foreach (var err in kv.Value.Errors)
                    Console.WriteLine($"Field {kv.Key}: {err.ErrorMessage}");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.JobTypes.Any(e => e.Id == jobType.Id))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(jobType);
        }

        // GET: JobTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var jobType = await _context.JobTypes.FirstOrDefaultAsync(m => m.Id == id);
            if (jobType == null) return NotFound();
            return View(jobType);
        }

        // POST: JobTypes/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobType = await _context.JobTypes.FindAsync(id);
            if (jobType != null)
            {
                _context.JobTypes.Remove(jobType);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}