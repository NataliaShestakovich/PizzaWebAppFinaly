﻿using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Infrastructure;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles;
using PizzaWebAppAuthentication.Options;
using PizzaWebAppAuthentication.Services.PizzaServises;

namespace PizzaWebAppAuthentication.Controllers
{
    public class CartController : Controller
    {
        private readonly IPizzaServices _pizzaServices;
        private readonly ApplicationDbContext _contextDb;
        private readonly PizzaOption _pizzaOption;
        public CartController(ApplicationDbContext contextDb,
                              PizzaOption pizzaOption,
                              IPizzaServices pizzaservices)
        {
            _contextDb = contextDb;
            _pizzaOption = pizzaOption;
            _pizzaServices = pizzaservices;
        }
       
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetJson <List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            CartViewModel cartViewModel = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity* x.Price)
            };

            return View(cartViewModel);
        }

        public async Task<IActionResult> Add(Guid id) 
        {
            //var pizza = await _contextDb.Pizzas.FindAsync(id); // Вынести в сервис и репозиторий этот метод и заменить на обращение через него
            var pizza = await _pizzaServices.GetStandartPizzaByIdAsync(id);
            if (pizza == null)
            {
                TempData["Error"] = _pizzaOption.SuccessAddPizzaInCart;
                return View(pizza);
            }
            var cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            CartItemViewModel cartItem = cart.Where(p => p.PizzaId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItemViewModel(pizza));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            TempData["Success"] = string.Format(_pizzaOption.SuccessAddPizzaInCart, pizza.Name);

            return Redirect(Request.Headers["Referer"].ToString());
        }
        
        public async Task<IActionResult> AddPizza (Pizza pizza) 
        {             
            if (pizza == null)
            {
                TempData["Error"] = _pizzaOption.SuccessAddPizzaInCart;
                return View(pizza);
            }
            var cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart") ?? new List<CartItemViewModel>();

            CartItemViewModel cartItem = cart.Where(p => p.PizzaId == pizza.Id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItemViewModel(pizza));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            TempData["Success"] = string.Format(_pizzaOption.SuccessAddCustomPizza, pizza.Name);

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult Decrease(Guid id)
        {
            var cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart");

            CartItemViewModel cartItem = cart.Where(p => p.PizzaId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p=>p.PizzaId==id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }
            
            TempData["Success"] = string.Format(_pizzaOption.SuccessDecreasePizzaInCart,cartItem.PizzaName);

            return RedirectToAction("Index");
        }

        public IActionResult Remove(Guid id)
        {
            var cart = HttpContext.Session.GetJson<List<CartItemViewModel>>("Cart");

            cart.RemoveAll(p => p.PizzaId == id);

            CartItemViewModel cartItem = cart.Where(p => p.PizzaId == id).FirstOrDefault();

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            string pizzaName = cart.Where(c => c.PizzaId == id).Select(c => c.PizzaName).FirstOrDefault();
            TempData["Success"] = String.Format(_pizzaOption.SuccessRemovePizzaFromCart, pizzaName);

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }
    }
}
