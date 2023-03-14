using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels;

namespace PizzaWebAppAuthentication.Controllers
{
    //контроллер, который будет отвечать за отображение списка доступных пицц и деталей
    //каждой пиццы, а также за добавление пиццы в корзину.

    public class PizzasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PizzasController(ApplicationDbContext context)
        {
            _context= context;
        }
        // GET: PizzaController
        public IActionResult Index() // отображает список доступных пицц ЮЗЕР И АДМИН Т.Е ЗАРЕГИСТРИРОВАННЫЕ ПОЛЬЗОВАТЕЛИ
        {
            return View();
        }

        // GET: PizzaController/Details/5
        public IActionResult Details(int id) //отображает детали конкретной пиццы, включая ее ингредиенты и цену. ЗАРЕГИСТРИРОВАННЫЕ ПОЛЬЗОВАТЕЛИ
        {
            return View();
        }

        public IActionResult AddToCart(int id) //добавляет пиццу в корзину заказов. ЗАРЕГИСТРИРОВАННЫЕ ПОЛЬЗОВАТЕЛИ ИЛИ ТОЛЬКО ЮЗЕР ???
        {
            return View();
        }

        // GET: PizzaController/Create
        public IActionResult Create() // отображает форму для создания новой пиццы. АДМИН
        {
            return View();
        }

        // POST: PizzaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
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

        [HttpGet]
        public IActionResult CreateCustomPizza() 
        {
            var ingredients = _context.Ingredients.Select(x => x.Name).ToList();
            ViewData["Ingredients"] = ingredients;

            var bases = _context.Bases.ToList(); ;
            ViewData["Bases"] = bases;

            var sizes = _context.Sizes.ToList();
            ViewData["Sizes"] = sizes;

            return View();
        }

        [HttpPost]
        public IActionResult CreateCustomPizza(PizzaViewModel pizza)
        {
            var newPizza = new Pizza();
            newPizza.Id = Guid.NewGuid();
            newPizza.Standart = true;
            newPizza.Name = "Клиентская";

            newPizza.PizzaBase = _context.Bases.Where(c => c.Name == pizza.Base).FirstOrDefault();

            newPizza.Size = _context.Sizes.Where(c => c.Diameter == pizza.Size).FirstOrDefault();

            decimal ingredientCost = 0;

            foreach (var item in pizza.Ingredients)
            {
                var ingredient = _context.Ingredients.Where(c => c.Name == item).FirstOrDefault();
                newPizza.Ingredients.Add(ingredient);
                ingredientCost += ingredient.Price;
            }

            newPizza.Price = newPizza.PizzaBase.Price + (decimal)newPizza.Size.Diameter * 0.1m + ingredientCost;

            //return RedirectToAction("Index", "Home");

            return Content($"Вы успешно сформировали пиццу. Стоимость: {newPizza.Price}");
        }

        // GET: PizzaController/Edit/5
        public IActionResult Edit(int id) //отображает форму для редактирования существующей пиццы АДМИН
        {
            return View();
        }

        // POST: PizzaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
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

        // GET: PizzaController/Delete/5
        public IActionResult Delete(int id) // удаляет выбранную пиццу в объект меню пиццерии АДМИН
        {
            return View();
        }

        // POST: PizzaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
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
