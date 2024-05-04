using Microsoft.AspNetCore.Mvc;

namespace FLO_Proyect.Controllers
{
    public class AccountController: Controller
    {
        public IActionResult Register()
        {
            return View();

        }
    }
}
