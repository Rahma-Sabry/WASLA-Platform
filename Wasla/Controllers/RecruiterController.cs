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
    public class RecruitersController : Controller
    {
        private readonly WaslaContext _context;

        public RecruitersController(WaslaContext context)
        {
            _context = context;
        }

        // GET: Recruiter
        public async Task<IActionResult> Index()
        {
            HttpContext.Session.SetString("UserId", "1");
            return View(await _context.Recruiters.ToListAsync());
        }

        // GET: Recruiter/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recruiter = await _context.Recruiters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recruiter == null)
            {
                return NotFound();
            }

            return View(recruiter);
        }

        // GET: Recruiter/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recruiter/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CompanyName,CompanyDesc,Id,FirstName,LastName,SSN,DateOfBirth")] Recruiter recruiter)
        {
            foreach (var kv in ModelState)
            {
                foreach (var err in kv.Value.Errors)
                {
                    Console.WriteLine($"Field {kv.Key}: {err.ErrorMessage}");
                }
            }

            if (ModelState.IsValid)    //we changed from ModelState.IsValid (didn't valid model)to !ModelState.IsValid (this work)
            {
                _context.Add(recruiter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            return View(recruiter);
        }

        ////GET: Recruiter/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var recruiter = await _context.Recruiters.FindAsync(id);
        //    if (recruiter == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(recruiter);
        //}



        //// POST: Recruiter/Edit/5

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("CompanyName,CompanyDesc,UserId,FirstName,LastName,Email,SSN,Phone,Password,DateOfBirth,Coins")] Recruiter recruiter)
        //{
        //    if (id != recruiter.UserId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(recruiter);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!RecruiterExists(recruiter.UserId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(recruiter);
        //}
        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
                    if (!_context.Users.Any(u => u.Id == user.Id))
                        return NotFound();
                    else
                        throw;
                }
            }

            return View(user);
        }

        // GET: Recruiter/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recruiter = await _context.Recruiters
                .FirstOrDefaultAsync(m => m.Id == id);
            if (recruiter == null)
            {
                return NotFound();
            }

            return View(recruiter);
        }

        // POST: Recruiter/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var recruiter = await _context.Recruiters.FindAsync(id);
            if (recruiter != null)
            {
                _context.Recruiters.Remove(recruiter);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecruiterExists(int id)
        {
            return _context.Recruiters.Any(e => e.Id == id);
        }
    }
}
