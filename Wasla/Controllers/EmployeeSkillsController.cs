using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Wasla.Models;

namespace Wasla.Controllers
{
    public class EmployeeSkillsController : Controller
    {
        private readonly WaslaContext _context;

        public EmployeeSkillsController(WaslaContext context)
        {
            _context = context;
        }

        // GET: EmployeeSkills
        public async Task<IActionResult> Index()
        {
            // mimic recruiter session setup
            HttpContext.Session.SetString("UserId", "1");
            var list = _context.Set<EmployeeSkill>()
                .Include(es => es.Employee)
                .Include(es => es.Skill);
            return View(await list.ToListAsync());
        }

        // GET: EmployeeSkills/Details?employeeId=1&skillId=2
        public async Task<IActionResult> Details(int? employeeId, int? skillId)
        {
            if (employeeId == null || skillId == null)
                return NotFound();

            var es = await _context.Set<EmployeeSkill>()
                .Include(e => e.Employee)
                .Include(e => e.Skill)
                .FirstOrDefaultAsync(m => m.EmployeeId == employeeId && m.SkillId == skillId);
            if (es == null)
                return NotFound();

            return View(es);
        }

        // GET: EmployeeSkills/Create
        public IActionResult Create()
        {
            // grab session for debug
            var uid = HttpContext.Session.GetString("UserId");
            Console.WriteLine("Current UserId session: " + uid);

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "UserId", "FirstName");
            ViewData["SkillId"] = new SelectList(_context.Skills, "SkillId", "SkillName");
            return View();
        }

        // POST: EmployeeSkills/Create
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,SkillId,YOE")] EmployeeSkill es)
        {
            // dump out any validation errors on server console
            foreach (var kv in ModelState)
                foreach (var err in kv.Value.Errors)
                    Console.WriteLine($"Field {kv.Key}: {err.ErrorMessage}");

            if (ModelState.IsValid)
            {
                _context.Add(es);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else { Console.WriteLine("validation error"); }
            // repopulate dropdowns if invalid
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "UserId", "FirstName", es.EmployeeId);
            ViewData["SkillId"] = new SelectList(_context.Skills, "SkillId", "SkillName", es.SkillId);
            return View(es);
        }

        // GET: EmployeeSkills/Edit?employeeId=1&skillId=2
        public async Task<IActionResult> Edit(int? employeeId, int? skillId)
        {
            if (employeeId == null || skillId == null)
                return NotFound();

            var es = await _context.Set<EmployeeSkill>()
                                  .FindAsync(employeeId, skillId);
            if (es == null)
                return NotFound();

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "UserId", "FirstName", es.EmployeeId);
            ViewData["SkillId"] = new SelectList(_context.Skills, "SkillId", "SkillName", es.SkillId);
            return View(es);
        }

        // POST: EmployeeSkills/Edit
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int employeeId, int skillId, [Bind("EmployeeId,SkillId,YOE")] EmployeeSkill es)
        {
            if (employeeId != es.EmployeeId || skillId != es.SkillId)
                return NotFound();

            foreach (var kv in ModelState)
                foreach (var err in kv.Value.Errors)
                    Console.WriteLine($"Field {kv.Key}: {err.ErrorMessage}");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(es);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Set<EmployeeSkill>().Any(x => x.EmployeeId == employeeId && x.SkillId == skillId))
                        return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["EmployeeId"] = new SelectList(_context.Employees, "UserId", "FirstName", es.EmployeeId);
            ViewData["SkillId"] = new SelectList(_context.Skills, "SkillId", "SkillName", es.SkillId);
            return View(es);
        }

        // GET: EmployeeSkills/Delete?employeeId=1&skillId=2
        public async Task<IActionResult> Delete(int? employeeId, int? skillId)
        {
            if (employeeId == null || skillId == null)
                return NotFound();

            var es = await _context.Set<EmployeeSkill>()
                .Include(e => e.Employee)
                .Include(e => e.Skill)
                .FirstOrDefaultAsync(m => m.EmployeeId == employeeId && m.SkillId == skillId);
            if (es == null)
                return NotFound();

            return View(es);
        }



        // POST: EmployeeSkills/Delete
        [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int employeeId, int skillId)
        {
            var es = await _context.Set<EmployeeSkill>().FindAsync(employeeId, skillId);
            if (es != null)
            {
                _context.Set<EmployeeSkill>().Remove(es);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
