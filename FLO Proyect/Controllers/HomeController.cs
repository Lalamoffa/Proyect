using FLO_Proyect.Models;
using FLO_Proyect.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FLO_Proyect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppdbContext context;

        public HomeController(ILogger<HomeController> logger, AppdbContext _context)
        {
            _logger = logger;
            context = _context;
        }

        public IActionResult Index()
        {
            //List<Slider> sliderlist= new List<Slider>();
            //Slider slider = new Slider()
            //{
            //    Id = 1,
            //    SubTitle = "Sale products",
            //    Title = "NIKE ARI MAX 2015",
            //    Description = "Lorem Ipsum is simply dummy text of the printing",
            //    ImgUrl = "slider-1.jpg"   ozumuz ucun yazirdiq bunu

            //};

            HomeVM vM = new HomeVM
            {
                Products = context.Products.Include(x => x.Images).Where(x => x.Ischeck == true).ToList(),
                Sliders = context.Sliders.Where(x => x.Ischeck == true).ToList()
            };

            return View(vM);
        }

        public IActionResult Privacy()
        {
            return View();

        }

        public IActionResult aboutus()
        {
            return View("about-us");

        }

        public IActionResult blog()
        {
            return View("blog");

        }

        public IActionResult blogdetails()
        {
            return View("blog-details");

        }

        public IActionResult cart()
        {
            return View("cart");

        }

        public IActionResult checkout()
        {
            return View("checkout");

        }

        public IActionResult contact()
        {
            return View("contact");

        }
        public IActionResult login()
        {
            return View("login");

        }
        public IActionResult myaccount()
        {
            return View("my-account");

        }
        public IActionResult register()
        {
            return View("register");

        }

        public IActionResult shop()
        {
            var model = new ShopVM
            {
                categoryes = context.Categories.Where(x => x.Ischeck == true).ToList(),
                products = context.Products.Include(prd => prd.Images).Where(x => x.Ischeck == true).ToList(),
                colors = context.Colors.ToList()
            };
            return View(model);

        }

        public IActionResult shoplist()
        {
            return View("shop-list");

        }

        public IActionResult Productdetail()
        {
            return View();
        }

        public IActionResult wishlist()
        {
            return View("wishlist");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
