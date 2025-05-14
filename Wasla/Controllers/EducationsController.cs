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
    public class EducationsController : Controller
    {
        private readonly WaslaContext _context;

        public EducationsController(WaslaContext context)
        {
            _context = context;
        }

        // GET: Educations
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("EmployeeId", "14");
            var waslaContext = _context.Education.Include(e => e.DegreeType).Include(e => e.Employee);

            return View(await waslaContext.ToListAsync());
        }

        // GET: Educations/Details/5
        public async Task<IActionResult> Details()
        {
            var employeeIdStr = HttpContext.Session.GetString("EmployeeId");
            int employeeId = int.Parse(employeeIdStr);

            var education = await _context.Education
                .Include(e => e.DegreeType)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.EmployeeId == employeeId);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // GET: Educations/Create
        public IActionResult Create()
        {
            HttpContext.Session.SetString("EmployeeId", "2");
            ViewData["DegreeId"] = new SelectList(_context.DegreeTypes, "Id", "DegreeName");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName");
            return View();
        }

        // POST: Educations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,StartDate,UniversityName,EndDate,DegreeId")] Education education)
        {
            foreach (var kv in ModelState)
            {
                foreach (var err in kv.Value.Errors)
                {
                    Console.WriteLine($"Field {kv.Key}: {err.ErrorMessage}");
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(education);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            ViewData["DegreeId"] = new SelectList(_context.DegreeTypes, "Id", "DegreeName", education.DegreeId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "UserId", "FullName", education.EmployeeId);
            return View(education);
        }

        // GET: Educations/Edit/5
        public async Task<IActionResult> Edit(int? EmployeeId, DateTime? StartDate)
        {
            if (EmployeeId == null || StartDate == null)
            {
                return NotFound();
            }

            var education = await _context.Education.FindAsync(EmployeeId, StartDate);
            if (education == null)
            {
                return NotFound();
            }
            ViewData["DegreeId"] = new SelectList(_context.DegreeTypes, "Id", "DegreeName", education.DegreeId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "UserId", "FullName", education.EmployeeId);
            return View(education);
        }

        // POST: Educations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DateTime startDate, [Bind("StartDate,UniversityName,EndDate,DegreeId")] Education education)
        {
            foreach (var kv in ModelState)
            {
                foreach (var err in kv.Value.Errors)
                {
                    Console.WriteLine($"Field {kv.Key}: {err.ErrorMessage}");
                }
            }
            if (ModelState.IsValid)
            {
                var employeeIdStr = HttpContext.Session.GetString("EmployeeId");

                int employeeId = int.Parse(employeeIdStr);
                education.EmployeeId = employeeId;
                try
                {
                    _context.Update(education);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool exists = await _context.Education.AnyAsync(e => e.EmployeeId == employeeId && e.StartDate == startDate);
                    if (!exists) return NotFound();
                    else throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["DegreeId"] = new SelectList(_context.DegreeTypes, "Id", "DegreeName", education.DegreeId);
            return View(education);
        }


        // GET: Educations/Delete/5
        public async Task<IActionResult> Delete(int? EmployeeId, DateTime? StartDate)
        {
            if (EmployeeId == null || StartDate == null)
            {
                return NotFound();
            }

            var education = await _context.Education
                .Include(e => e.DegreeType)
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.EmployeeId == EmployeeId);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // POST: Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed()
        {
            var employeeIdStr = HttpContext.Session.GetString("EmployeeId");
            int employeeId = int.Parse(employeeIdStr);
            var education = await _context.Education.FindAsync(employeeId);
            if (education != null)
            {
                _context.Education.Remove(education);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationExists(int id)
        {
            return _context.Education.Any(e => e.EmployeeId == id);
        }
    }
}
