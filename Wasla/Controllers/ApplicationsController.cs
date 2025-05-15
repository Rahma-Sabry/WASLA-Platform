using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wasla.Models;

namespace Wasla.Controllers
{
    public class ApplicationsController : Controller
    {
        private readonly WaslaContext _context;

        public ApplicationsController(WaslaContext context)
        {
            _context = context;
        }

        // GET: Applications
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("userId","3");
            var waslaContext = _context.Applies.Include(a => a.Employee).Include(a => a.Job);
            return View(await waslaContext.ToListAsync());
        }

        // GET: Applications/Details/5
        public async Task<IActionResult> Details(int employeeId, int jobId)
        {
            var application = await _context.Applies
                .Include(a => a.Employee)
                .Include(a => a.Job)
                .FirstOrDefaultAsync(m => m.EmployeeId == employeeId && m.JobId == jobId);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        // GET: Applications/Create
        public IActionResult Create()
        {
            var jobs = _context.Jobs.ToList(); 
            HttpContext.Session.SetString("userId", "5");
            ViewData["JobId"] = new SelectList(jobs, "JobId", "JobTitle");
            return View();
        }

        // POST: Applications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobId,ApplyDate")] Application application)
        {
            application.EmployeeId = int.Parse(HttpContext.Session.GetString("userId"));
            
            if (ModelState.IsValid)
            {
                _context.Add(application);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["JobId"] = new SelectList(_context.Jobs, "JobId", "JobTitle", application.JobId);
            return View(application);
        }

        // GET: Applications/Edit/5
        public async Task<IActionResult> Edit(int employeeId, int jobId)
        {
            var application = await _context.Applies
                .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.JobId == jobId);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int employeeId, int jobId, Application application)
        {
            if (employeeId != application.EmployeeId || jobId != application.JobId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(application);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Applies.Any(a => a.EmployeeId == employeeId && a.JobId == jobId))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(application);
        }

        public async Task<IActionResult> Delete(int employeeId, int jobId)
        {
            var application = await _context.Applies
                .Include(a => a.Employee)
                .Include(a => a.Job)
                .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.JobId == jobId);

            if (application == null)
            {
                return NotFound();
            }

            return View(application);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int employeeId, int jobId)
        {
            var application = await _context.Applies
                .FirstOrDefaultAsync(a => a.EmployeeId == employeeId && a.JobId == jobId);

            if (application != null)
            {
                _context.Applies.Remove(application);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ApplicationExists(int id)
        {
            return _context.Applies.Any(e => e.EmployeeId == id);
        }
    }
}
