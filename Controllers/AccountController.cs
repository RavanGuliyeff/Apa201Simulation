using FinalExamSimulation.Models;
using FinalExamSimulation.Utilities.Enums;
using FinalExamSimulation.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace FinalExamSimulation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterVm registerVM)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = new AppUser()
            {
                Fullname = registerVM.Fullname,
                Email = registerVM.Email,
                UserName = registerVM.UserName
            };

            string userRole = UserRole.Member.ToString();


            //Bu şərti yazıraq ki, ilk qeydiyyat olan istifadəçi admin olsun(Əgər öncədən heç bir istifadəçi yox idisə)
            if (!await _userManager.Users.AnyAsync())
            {
                userRole = UserRole.Admin.ToString();
            }

            IdentityResult result = await _userManager.CreateAsync(user, registerVM.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    return View();
                }
            }

            await _userManager.AddToRoleAsync(user, userRole); // Bayaq qeyd etdiyimiz userRole dəyişəni ilə istifadəçiyə rol təyin edirik

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginVm loginVM)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginVM.UserNameOrEmail || u.Email == loginVM.UserNameOrEmail);

            if (user == null)
            {
                ModelState.AddModelError("", "username or mail invalid");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.IsPersisted, true);

            if (!result.Succeeded) // burada ! yaddan cixmisdi
            {
                ModelState.AddModelError("", "password invalid");
                return View();
            }


            return RedirectToAction("index", "Home");
        }

        public async Task<IActionResult> CreateRoles()
        {
            foreach (var role in Enum.GetNames(typeof(UserRole)))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new IdentityRole(role));
                }
            }
            
            return Content("Rollar yaradıldı!");
        }
    }

}
