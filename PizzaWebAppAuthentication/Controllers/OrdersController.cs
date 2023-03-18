using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Infrastructure;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles;
using PizzaWebAppAuthentication.Services.PizzaServises;

namespace PizzaWebAppAuthentication.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPizzaServices _pizzaServises;

        public OrdersController(ILogger<HomeController> logger,
                                ApplicationDbContext context,
                                UserManager<ApplicationUser> userManager,
                                IPizzaServices pizzaServises)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _pizzaServises = pizzaServises;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            if (cart.Count <= 0 || cart == null)
            {
                TempData["Error"] = "Your cart is empty, add some pizzas first";

                return RedirectToAction("Index", "Home");
            }

             return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(Order dataOrder)
        {
            var id = Guid.NewGuid();

            var cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            if (cart.Count >= 0 || cart != null)
            {
                Order order = new Order
                {
                    Id = id,
                    OrderDate = DateTime.Now,
                    CartItems = new List<CartItem>(),
                    TotalPrice = cart.Sum(x => x.Quantity * x.Price)
                };

                string userName = HttpContext.User.Identity.Name;
                              
                var user = await _userManager.FindByNameAsync(userName);

                order.ApplicationUserId = Guid.Parse(user.Id);

                var IsExistAddress = _context.Addresses.Where(c => c.City == dataOrder.DeliveryAddress.City).
                                                        Where(s => s.Street == dataOrder.DeliveryAddress.Street).
                                                        Where(b => b.Building == dataOrder.DeliveryAddress.Building).
                                                        Where(a => a.Apartment == dataOrder.DeliveryAddress.Apartment).
                                                        FirstOrDefault();
                order.DeliveryAddress = new();

                if (IsExistAddress != null)
                {
                    order.DeliveryAddress = IsExistAddress;
                }
                else
                {
                    order.DeliveryAddress.Id = Guid.NewGuid();
                    order.DeliveryAddress.City = dataOrder.DeliveryAddress.City;
                    order.DeliveryAddress.Street = dataOrder.DeliveryAddress.Street;
                    order.DeliveryAddress.Building = dataOrder.DeliveryAddress.Building;
                    order.DeliveryAddress.Apartment = dataOrder.DeliveryAddress.Apartment;
                }

                var customPizzas = HttpContext.Session.GetJson<List<Pizza>>("CustomPizza") ?? new List<Pizza>();

                foreach (var cartItem in cart)
                {
                    if ((customPizzas != null || customPizzas.Count()>0) && customPizzas.Any(x => x.Id == cartItem.PizzaId))
                    {
                        var customPizza = customPizzas.Find(p => p.Id == cartItem.PizzaId);

                        _context.Pizzas.Add(customPizza);
                        _context.Entry(customPizza.Size).State = EntityState.Unchanged;
                        _context.Entry(customPizza.PizzaBase).State = EntityState.Unchanged;
                        foreach (var item in customPizza.Ingredients)
                        {
                            _context.Entry(item).State = EntityState.Unchanged;
                        }
                        
                        _context.SaveChanges();                        
                    }
                    var orderItem = new CartItem
                    {
                        ID = cartItem.ID,
                        PizzaId = cartItem.PizzaId,
                        Quantity = cartItem.Quantity,
                        OrderId = order.Id
                    };
                    
                    order.CartItems.Add(orderItem);
                }

                if (ModelState.IsValid)
                {
                    await _context.Orders.AddAsync(order);
                    await _context.SaveChangesAsync();

                    HttpContext.Session.Remove("Cart");

                    TempData["Success"] = "Your order is accepted";

                    return RedirectToAction("Index", "Home");
                }
            }            
            TempData["Error"] = "Incomplete ordering data";

            return RedirectToAction("Index","Cart");
        }
    }
}
