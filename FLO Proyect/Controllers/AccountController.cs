using FLO_Proyect.Extensions;
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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ProgramUser> _signInManager;
        private readonly IEmailService emailService;


        public AccountController(AppdbContext _appDbContext, UserManager<ProgramUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<ProgramUser> signInManager, IEmailService _emailService)
        {
            appDbContext = _appDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            emailService = _emailService;
            _roleManager = roleManager;
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
                await _userManager.AddToRoleAsync(programUser, "User");
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(programUser);
                var confirmationLink = Url.Action("Index", "Home", new { userId = programUser.Id, token = token }, Request.Scheme);
                await emailService.SendEmailAsync(model.Email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");
                await _signInManager.SignInAsync(programUser, true);

                return RedirectToAction("Index", "Home");
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
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task SeedRoles()
        {
            if (!await _roleManager.RoleExistsAsync(roleName: "Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName: "Admin"));
            }
            if (!await _roleManager.RoleExistsAsync(roleName: "User"))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName: "User"));
            }
        }

        public async Task SeedAdmin()
        {
            if (_userManager.FindByEmailAsync("mammadoffa.lala@gmail.com").Result == null)
            {
                ProgramUser programUser = new ProgramUser
                {
                    Email = "mammadoffa.lala@gmail.com",
                    UserName = "mammadoffa.lala@gmail.com"
                };
                var result = await _userManager.CreateAsync(programUser, "lola0205!A");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(programUser, "Admin");
                    await _signInManager.SignInAsync(programUser, true);

                    RedirectToAction("Index", "Home");
                }
            }
        }

    }
}
