﻿using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Infrastructure;
using PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles;

namespace PizzaWebAppAuthentication.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _contextDb;
        public CartController(ApplicationDbContext contextDb)
        {
            _contextDb = contextDb;
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
            var pizza = await _contextDb.Pizzas.FindAsync(id); // Вынести в сервис и репозиторий этот метод и заменить на обращение через него
            if (pizza == null)
            {
                //TODO сделать проверку выбросить нотификейшен
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

            TempData["Success"] = "The product has been added.";

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
            
            TempData["Success"] = "The product has been removed.";

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

            TempData["Success"] = "The product has been removed.";

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }


    }
}
