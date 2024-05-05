using FLO_Proyect.Models;
using FLO_Proyect.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FLO_Proyect.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppdbContext appDbContext;
        private readonly UserManager<ProgramUser> _userManager;
        private readonly SignInManager<ProgramUser> _signInManager;

        public AccountController(AppdbContext _appDbContext, UserManager<ProgramUser> userManager, SignInManager<ProgramUser> signInManager)
        {
            appDbContext = _appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ProgramUser programUser = new ProgramUser
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(programUser, model.Password);
            if (result.Succeeded)
            {
                RedirectToAction("Index", "Home");
            }
            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something incorrect");

            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRemember, false);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Pssword or Email incorrect");
                }
            }

            return RedirectToAction("Index", "Home");
        }


        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
