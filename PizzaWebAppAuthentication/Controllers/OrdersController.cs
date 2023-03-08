using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Infrastructure;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles;
using PizzaWebAppAuthentication.Repositories;

namespace PizzaWebAppAuthentication.Controllers
{
    //контроллер, который будет отвечать за отображение и управление заказами клиентов, а также
    //за обработку заказов и отправку уведомлений о новых заказах на почту пиццерии.
    public class OrdersController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrdersController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            var id = Guid.NewGuid();
           
            Order order = new Order
            {
                Id = id,
                OrderDate = DateTime.Now,
                OrderItems = cart,
                TotalPrice = cart.Sum(x => x.Quantity * x.Price)
            };

            var userId = _userManager.GetUserId(HttpContext.User);
            
            order.User = new ApplicationUser
            {
                Id = userId
            };
            
            if (order.OrderItems.Count <= 0)
            {
                TempData["Error"] = "Your cart is empty, add some pizzas first";
                
                return RedirectToAction("Index", "Home");
            }
            return View(order);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order order)
        {
            Address address = new() 
            { 
                Id = Guid.NewGuid()
            };

            address.Orders.Add(order);
            address.User.Add(order.User);

            order.DeliveryAddress = address;

            if (ModelState.IsValid)
            {
                await _context.Orders.AddAsync(order);
                await _context.SaveChangesAsync();

                HttpContext.Session.Remove("Cart");
                
                TempData["Success"] = "Your order is accepted";

                return RedirectToAction("Index", "Home");
            }
            
            TempData["Error"] = "Incomplete ordering data";
            return View(order);
        }
    }
}
