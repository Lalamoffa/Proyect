//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Authorization;
//using Stripe.Checkout;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using FLO_Proyect.Models;

//namespace FLO_Proyect.Controllers
//{
//    [Authorize]
//    public class SalesController : Controller
//    {
//        private readonly AppdbContext _context;
//        private readonly UserManager<ProgramUser> _userManager;
//        public SalesController(AppdbContext context, UserManager<ProgramUser> userManager)
//        {
//            _context = context;
//            _userManager = userManager;
//        }
//        private static Models.Orders tempOrder;
//        private static decimal total = 0;
//        [HttpPost]
//        public IActionResult Checkout(Models.Orders order, string code)
//        {
//            var list = HttpContext.Session.GetJson<List<CartItem>>("Cart");
//            decimal prcntg = 0;
//            if (list == null)
//            {
//                return RedirectToAction("ShopCart", "Cart");
//            }
//            if (code != null)
//            {
//                var proCode = _context.Promotions.FirstOrDefault(p => p.Code == code);
//                prcntg = proCode == null ? 0 : proCode.DiscountPercentage;
//            }
//            tempOrder = order;
//            var domain = "https://localhost:44325/";
//            var options = new SessionCreateOptions()
//            {
//                SuccessUrl = domain + $"CheckOut/OrderConfirmation",
//                CancelUrl = domain + "CheckOut/Login",
//                LineItems = new List<SessionLineItemOptions>(),
//                Mode = "payment"
//            };
//            foreach (var item in list)
//            {
//                var sessionListItem = new SessionLineItemOptions
//                {
//                    PriceData = new SessionLineItemPriceDataOptions
//                    {
//                        UnitAmountDecimal = item.Price * 100 * (1 - prcntg / 100),
//                        Currency = "usd",
//                        ProductData = new SessionLineItemPriceDataProductDataOptions
//                        {
//                            Name = item.ProductName
//                        }
//                    },
//                    Quantity = item.Quantity
//                };
//                total += item.Total * (1 - prcntg / 100);
//                options.LineItems.Add(sessionListItem);
//            }
//            var service = new SessionService();
//            Session session = service.Create(options);
//            TempData["Session"] = session.Id;
//            Response.Headers.Add("Location", session.Url);
//            return new StatusCodeResult(303);
//        }
//        [HttpGet]
//        public IActionResult Billing()
//        {
//            return View();
//        }
//        public async Task<IActionResult> OrderConfirmation()
//        {
//            var service = new SessionService();
//            Session session = service.Get(TempData["Session"].ToString());
//            if (session.PaymentStatus == "paid")
//            {
//                var user = await _userManager.GetUserAsync(User);
//                var random = new Random();
//                tempOrder.UserId = user.Id;
//                tempOrder.OrdersNumber = random.Next(100000, 1000000);
//                tempOrder.Date = DateTime.Now;
//                tempOrder.Total = total;

//                _context.Orders.Add(tempOrder);
//                _context.SaveChanges();
//                var cartList = HttpContext.Session.GetJson<List<CartItem>>("Cart");
//                foreach (var item in cartList)
//                {
//                    var orderDetail = new OrdersItem
//                    {
//                        OrderId = tempOrder.Id,
//                        ProductId = item.ProductId,
//                        Quantity = item.Quantity,
//                        Subtotal = item.Total,
//                        Color = item.Color,
//                        Size = item.Size
//                    };
//                    _context.OrdersItems.Add(orderDetail);
//                    var product = _context.Products.FirstOrDefault(p => p.Id == item.ProductId);
//                    product.StockQuantity -= item.Quantity;
//                }
//                _context.SaveChanges();
//                var _order = _context.Orders.Include(x => x.OrdersItems).ThenInclude(x => x.Product).FirstOrDefault(x => x.Id == tempOrder.Id);
//                return View("Success", _order);
//            }
//            return View("Fail");
//        }



//    }
//}