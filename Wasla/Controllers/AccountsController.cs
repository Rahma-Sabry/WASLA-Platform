using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wasla.Models;

namespace Wasla.Controllers
{
    public class AccountsController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public AccountsController(
            UserManager<IdentityUser> um,
            RoleManager<IdentityRole> rm,
            SignInManager<IdentityUser> sm)
        {
            userManager = um;
            roleManager = rm;
            signInManager = sm;
        }

        public IActionResult Index()
        {
            var users = userManager.Users.ToList();
            var viewModels = users.Select(user => new RegisterViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                
            });
            return View(viewModels);
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(model == null)
            {
                return NoContent();
            }

            var newUser = new IdentityUser() {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
            };

            var result = await userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                IdentityRole role = new IdentityRole();
                if (model.isRecruiter)
                {
                    
                    role.Name = "Recruiter";
                }
                else
                {
                    role.Name = "Employee";
                }
                
                var roleResult = await roleManager.CreateAsync(role);
                await userManager.AddToRoleAsync(newUser, role.Name);
                var data = new
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    SSN = model.SSN,
                    DateOfBirth = model.DateOfBirth.ToString("yyyy-MM-dd")
                };

                if (model.isRecruiter)
                {
                    return RedirectToAction("Create", "Recruiters", data);
                }
                else
                {
                    return RedirectToAction("Create", "Employees", data);
                }
            }
            ModelState.AddModelError("", "Invalid Register Steps");
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string ReturnUrl)
        {
            //var currentUser=await userManager.FindByEmailAsync(model.Email);
            //if (currentUser == null) {return NotFound();}
            // await  signInManager.SignInAsync(currentUser,isPersistent:model.Remember);
            var result = await signInManager.
                   PasswordSignInAsync( // Find Email, Check Password
                model.UserName, model.Password, model.Remember, false);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                {
                    return LocalRedirect(ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError("", "Invalid Login !!");
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Signout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
