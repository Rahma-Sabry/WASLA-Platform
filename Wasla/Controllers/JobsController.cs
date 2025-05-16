using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wasla.Models;

namespace Wasla.Controllers
{
    public class JobsController : Controller
    {
        private readonly WaslaContext _context;
        public JobsController(WaslaContext context) => _context = context;

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(HttpContext.Session.GetString("Email")));
            HttpContext.Session.SetString("UserId", user.Id.ToString());
            var list = _context.Jobs
                .Include(j => j.Recruiter)
                .Include(j => j.JobType);
            return View(await list.ToListAsync());
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var job = await _context.Jobs
                .Include(j => j.Recruiter)
                .Include(j => j.JobType)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (job == null) return NotFound();
            return View(job);
        }

        // GET: Jobs/Create
        [Authorize(Roles = "Recruiter")]
        public IActionResult Create()
        {
            // optional seed of JobTypes
            if (!_context.JobTypes.Any())
            {
                _context.JobTypes.AddRange(
                    new JobType { TypeName = "Full-Time" },
                    new JobType { TypeName = "Part-Time" },
                    new JobType { TypeName = "Internship" },
                    new JobType { TypeName = "Remote" }
                );
                _context.SaveChanges();
            }

            Console.WriteLine("Session UserId = " + HttpContext.Session.GetString("UserId"));
            ViewData["TypeId"] = new SelectList(_context.JobTypes.ToList(), "Id", "TypeName");

            return View();
        }

        // POST: Jobs/Create
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> Create(
            [Bind("JobTitle,Requirements,Salary,TypeId,Field,ExpireDate")] Job job)
        {
            job.RecruiterId = int.Parse(HttpContext.Session.GetString("UserId"));
            Console.WriteLine("Session UserId = " + HttpContext.Session.GetString("UserId"));
            foreach (var kv in ModelState)
                foreach (var err in kv.Value.Errors)
                    Console.WriteLine($"  ModelState Error → {kv.Key}: {err.ErrorMessage}");

            if (ModelState.IsValid)
            {
                // Explicitly map your form-bound TypeId into the real FK JobTypeId
                _context.Entry(job).Property("JobTypeId").CurrentValue = job.TypeId;

                _context.Add(job);
                await _context.SaveChangesAsync();
                Console.WriteLine("[POST Jobs/Create] Saved.");
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("[POST Jobs/Create] Validation failed.");
            ViewData["RecruiterId"] = new SelectList(_context.Recruiters.ToList(), "UserId", "CompanyName", job.RecruiterId);
            ViewData["TypeId"] = new SelectList(_context.JobTypes.ToList(), "Id", "TypeName", job.TypeId);
            return View(job);
        }

        // GET: Jobs/Edit/5
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) return NotFound();

            //ViewData["RecruiterId"] = new SelectList(_context.Recruiters.ToList(), "UserId", "CompanyName", job.RecruiterId);
            ViewData["TypeId"] = new SelectList(_context.JobTypes.ToList(), "Id", "TypeName");
            return View(job);
        }

        // POST: Jobs/Edit/5
        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles ="Recruiter")]
        public async Task<IActionResult> Edit(int id,
            [Bind("JobId,JobTitle,Requirements,Salary,TypeId,Field,ExpireDate")] Job job)
        {
            job.RecruiterId = int.Parse(HttpContext.Session.GetString("UserId"));
            if (id != job.JobId) return NotFound();
            foreach (var kv in ModelState)
                foreach (var err in kv.Value.Errors)
                    Console.WriteLine($"  ModelState Error → {kv.Key}: {err.ErrorMessage}");

            if (ModelState.IsValid)
            {
                // map TypeId → shadow FK
                _context.Entry(job).Property("JobTypeId").CurrentValue = job.TypeId;

                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Jobs.Any(e => e.JobId == job.JobId))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            //ViewData["RecruiterId"] = new SelectList(_context.Recruiters.ToList(), "UserId", "CompanyName", job.RecruiterId);
            ViewData["TypeId"] = new SelectList(_context.JobTypes.ToList(), "Id", "TypeName");
            return View(job);
        }

        // GET: Jobs/Delete/5
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var job = await _context.Jobs
                .Include(j => j.Recruiter)
                .Include(j => j.JobType)
                .FirstOrDefaultAsync(m => m.JobId == id);
            if (job == null) return NotFound();
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job != null)
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
