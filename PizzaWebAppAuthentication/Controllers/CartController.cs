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


        // GET: CartController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
