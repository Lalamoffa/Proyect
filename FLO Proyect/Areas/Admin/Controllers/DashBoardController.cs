using Microsoft.AspNetCore.Mvc;

namespace FLO_Proyect.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
