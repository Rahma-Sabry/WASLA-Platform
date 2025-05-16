using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wasla.Models;

namespace Wasla.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly WaslaContext _context;

        public ProfilesController(WaslaContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Equals(HttpContext.Session.GetString("Email")));
            HttpContext.Session.SetString("ID", user.Id.ToString());
            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == user.Id);

            var recruiter = await _context.Recruiters
                .FirstOrDefaultAsync(m => m.Id == user.Id);

            if (employee != null)
            {
                return RedirectToAction("EmployeeProfile");
            }
            else if(recruiter != null)
            {
                return RedirectToAction("RecuiterProfile");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult EmployeeProfile()
        {
            var id = int.Parse(HttpContext.Session.GetString("ID"));
            ViewData["EmployeeId"] = id;
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            return View(employee);
        }
        public IActionResult RecuiterProfile()
        {
            var id = int.Parse(HttpContext.Session.GetString("ID"));
            ViewData["RecruiterId"] = id;
            var recruiter = _context.Recruiters.FirstOrDefault(e => e.Id == id);
            return View(recruiter);
        }
    }
}
