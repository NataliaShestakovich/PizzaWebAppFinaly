using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles;

namespace PizzaWebAppAuthentication.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _session;
        public CartController(IHttpContextAccessor session)
        {
            _session= session;
        }
        public ActionResult CartPartial()
        {
            //var model = new CartViewModel
            //{
            //    Items = new Dictionary<Pizza, int>()                 
            //};

            //model.User = new ApplicationUser();

            //if (HttpContext. != 0)
            //{
            //    var list = (List<CartViewModel>)Session["cart"];
            //}
            return PartialView();
        }

        
        // GET: CartController
        public ActionResult Index()
        {

            CartViewModel cartViewModel = new();
            return View(cartViewModel);
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
