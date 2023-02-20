using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PizzaWebAppAuthentication.Controllers
{
    //контроллер, который будет отвечать за отображение списка доступных ингредиентов и
    //деталей каждого ингредиента, а также за добавление ингредиента в корзину.
    
    public class IngredientsController : Controller
    {
        // GET: IngredientsController
        public ActionResult Index() // отображает список доступных ингредиентов Юзер редиректается
                                    // сюда из метода добавить кастомную пиццу или это должено быть
                                    // частичное представление
        {
            return View();
        }

        // GET: IngredientsController/Details/5
        public ActionResult Details(int id) // отображает детали конкретного ингредиента, включая
                                            // его описание и цену.

        {
            return View();
        }

        // GET: IngredientsController/Create
        public ActionResult Create() // отображает форму для создания нового ингредиента
        {
            return View();
        }

        // POST: IngredientsController/Create
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

        // POST: IngredientsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddIngredientToPizza(IFormCollection collection) // сюда нужно редиректоть при выборе ингридиентов для кастомной пиццы из контроллера пицц.
        {
            // Метод AddIngredientToPizza может принимать объект Pizza, а также список
            // ингредиентов, которые можно выбрать через чекбоксы.Этот метод должен отрисовать
            // в представлении форму, в которой можно выбрать нужные ингредиенты для пиццы и
            // отправить их на сервер.После получения выбранных ингредиентов метод
            // AddIngredientToPizza должен добавить их в объект Pizza и сохранить его в базе данных.
            // При успешном сохранении, метод может вернуть Redirect к странице корзины,
            // где пользователь сможет увидеть добавленную в корзину пиццу и ее стоимость.
           // После того, как пользователь выбрал все необходимые
        //ингредиенты, он может добавить созданную пиццу в корзину, используя действие "AddToCart".
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       
        // GET: IngredientsController/Edit/5
        public ActionResult Edit(int id) //отображает форму для редактирования существующего ингредиента.
        {
            return View();
        }

        // POST: IngredientsController/Edit/5
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

        // GET: IngredientsController/Delete/5
        public ActionResult Delete(int id) // удаляет выбранный ингредиент
        {
            return View();
        }

        // POST: IngredientsController/Delete/5
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
