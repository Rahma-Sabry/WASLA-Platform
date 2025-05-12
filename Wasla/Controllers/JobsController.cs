using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wasla.Models;

namespace Wasla.Controllers
{
    public class JobController : Controller
    {
        private readonly WaslaContext _context;

        public JobController(WaslaContext context)
        {
            _context = context;
        }

        // GET: Job
        public async Task<IActionResult> Index()
        {
            var jobs = _context.Jobs
                .Include(j => j.Recruiter)
                .Include(j => j.JobType);

            return View(await jobs.ToListAsync());
        }

        // GET: Job/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var job = await _context.Jobs
                .Include(j => j.Recruiter)
                .Include(j => j.JobType)
                .FirstOrDefaultAsync(m => m.JobId == id);

            if (job == null)
                return NotFound();

            return View(job);
        }

        // GET: Job/Create
        public IActionResult Create()
        {
            ViewData["RecruiterId"] = new SelectList(_context.Recruiters, "RecruiterId", "Name");
            ViewData["TypeId"] = new SelectList(_context.JobTypes, "TypeId", "Name");
            return View();
        }

        // POST: Job/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobId,RecruiterId,JobTitle,Requirements,Salary,TypeId,Field,ExpireDate")] Job job)
        {
            if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["RecruiterId"] = new SelectList(_context.Recruiters, "RecruiterId", "Name", job.RecruiterId);
            ViewData["TypeId"] = new SelectList(_context.JobTypes, "TypeId", "Name", job.TypeId);
            return View(job);
        }

        // GET: Job/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
                return NotFound();

            ViewData["RecruiterId"] = new SelectList(_context.Recruiters, "RecruiterId", "Name", job.RecruiterId);
            ViewData["TypeId"] = new SelectList(_context.JobTypes, "TypeId", "Name", job.TypeId);
            return View(job);
        }

        // POST: Job/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("JobId,RecruiterId,JobTitle,Requirements,Salary,TypeId,Field,ExpireDate")] Job job)
        {
            if (id != job.JobId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.JobId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["RecruiterId"] = new SelectList(_context.Recruiters, "RecruiterId", "Name", job.RecruiterId);
            ViewData["TypeId"] = new SelectList(_context.JobTypes, "TypeId", "Name", job.TypeId);
            return View(job);
        }

        // GET: Job/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var job = await _context.Jobs
                .Include(j => j.Recruiter)
                .Include(j => j.JobType)
                .FirstOrDefaultAsync(m => m.JobId == id);

            if (job == null)
                return NotFound();

            return View(job);
        }

        // POST: Job/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.JobId == id);
        }
    }
}
