using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Infrastructure;
using PizzaWebAppAuthentication.Models.AppModels;
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
       
        public ActionResult Index()
        {
            var cart = HttpContext.Session.GetJson <List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartViewModel = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity* x.Price)
            };

            return View(cartViewModel);
        }

        public async Task<ActionResult> Add(int id) 
        { 
            var pizza = await _contextDb.Pizzas.FindAsync(id); // Вынести в сервис и репозиторий этот метод и заменить на обращение через него
            if (pizza == null)
            {
                //TODO сделать проверку выбросить нотификейшен
            }
            var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(p => p.PizzaId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(pizza));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            TempData["Success"] = "The product has been added.";

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<ActionResult> Decrease(int id)
        {
            var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(p => p.PizzaId == id).FirstOrDefault();

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

        public async Task<ActionResult> Remove(int id)
        {
            var cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(p => p.PizzaId == id);

            CartItem cartItem = cart.Where(p => p.PizzaId == id).FirstOrDefault();

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

        public ActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }


    }
}
