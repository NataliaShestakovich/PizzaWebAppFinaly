using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PizzaWebAppAuthentication.Controllers
{
    //контроллер, который будет отвечать за отображение списка доступных пицц и деталей
    //каждой пиццы, а также за добавление пиццы в корзину.

    public class PizzasController : Controller
    {
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
        public IActionResult CreateCustomPizza() //отображать форму, на которой пользователь может
                                                 //выбирать свои предпочтения по ингредиентам для
                                                 //пиццы, включая тип теста, соуса, сыра и топпингов.
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCustomPizza(IFormCollection collection)
        {
            //Метод CreateCustomPizza может создавать объект Pizza, но он не обязан его
            //инициализировать пустым конструктором. В этом методе можно создавать объект
            //Pizza с некоторыми начальными значениями, например, задавать имя пиццы, ее размер
            //и т.д. Также метод CreateCustomPizza может возвращать не только RedirectToAction
            //к методу AddIngredientToPizza, но и сам объект Pizza, который затем будет
            //использоваться в методе AddIngredientToPizza.
            return View();
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
