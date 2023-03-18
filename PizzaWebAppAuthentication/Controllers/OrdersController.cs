using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Infrastructure;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles;
using PizzaWebAppAuthentication.Options;
using PizzaWebAppAuthentication.Services.PizzaServises;

namespace PizzaWebAppAuthentication.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IPizzaServices _pizzaServises;
        private readonly PizzaOption _pizzaOption;

        public OrdersController(ILogger<HomeController> logger,
                                UserManager<ApplicationUser> userManager,
                                IPizzaServices pizzaServises,
                                PizzaOption pizzaOption)
        {
            _logger = logger;
            _userManager = userManager;
            _pizzaServises = pizzaServises;
            _pizzaOption = pizzaOption;
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            if (cart.Count <= 0 || cart == null)
            {
                TempData["Error"] = _pizzaOption.EmptyCart;

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

                var IsExistAddress = await _pizzaServises.GetAddressAsync(dataOrder);
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

                        await _pizzaServises.AddCustomPizzaToDatabaseAsync(customPizza);                   
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
                    try
                    {
                        await _pizzaServises.AddOrderToDatabase(order);
                        TempData["Success"] = _pizzaOption.SuccessAcceptOrder;
                        HttpContext.Session.Remove("Cart");
                    }
                    catch (Exception)
                    {
                        TempData["Error"] = _pizzaOption.ErrorAddOrderToDatabase;
                    }
                    return RedirectToAction("Index", "Home");
                }
            }            
            TempData["Error"] = _pizzaOption.ErrorAcceptOrder;

            return RedirectToAction("Index","Cart");
        }
    }
}
