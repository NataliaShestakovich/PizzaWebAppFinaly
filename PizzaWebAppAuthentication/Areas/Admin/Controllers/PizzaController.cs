using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels.PizzaViewModels;
using PizzaWebAppAuthentication.Services.PizzaServises;

namespace PizzaWebAppAuthentication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PizzaController : Controller
    {
        private readonly IPizzaServices _pizzaServices;
       
        public PizzaController(IPizzaServices pizzaservices)
        {
            _pizzaServices = pizzaservices;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pizzaServices.GetStandartPizzasAsync());
        }

        public IActionResult Create()
        {
            var ingredients = _pizzaServices.GetIngredients();
            ViewData["Ingredients"] = ingredients;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PizzaViewModelForAdmin pizzaViewModel)
        {
            var ingredients = _pizzaServices.GetIngredients();
            ViewData["Ingredients"] = ingredients;
            Pizza newPizza = new Pizza();

            var existingPizzas = _pizzaServices.GetPizzasByName(pizzaViewModel.Name);
           
            if (existingPizzas.Count > 0)
            {
                ModelState.AddModelError("", $"Pizza {pizzaViewModel.Name} already exists");
                return View(pizzaViewModel);
            }

            newPizza.Name = pizzaViewModel.Name;
            newPizza.Price = pizzaViewModel.Price;
            newPizza.Standart = true;
            newPizza.PizzaBase = _pizzaServices.GetPizzaBaseByName("стандартная");
            newPizza.Size = _pizzaServices.GetSizeByDiameter(32);
            newPizza.Ingredients = new List<Ingredient>();

            if (ModelState.IsValid)
            {
                if (pizzaViewModel.ImageUpload != null)
                {
                    newPizza.ImagePath = await _pizzaServices.AddNewPizzaImageAsync(pizzaViewModel.ImageUpload);
                }

                if (pizzaViewModel.Ingredients == null || pizzaViewModel.Ingredients.Count == 0)
                {
                    TempData["Error"] = "Не добален ни один ингредиент";

                    return View(pizzaViewModel);
                }

                foreach (var item in pizzaViewModel.Ingredients)
                {
                    Ingredient ingredient = _pizzaServices.GetIngredientByName(item);
                    newPizza.Ingredients.Add(ingredient);
                }

                TempData["Success"] = await _pizzaServices.AddPizzaToDataBaseAsync(newPizza);

                return RedirectToAction("Index");
            }

            return View(pizzaViewModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            Pizza pizza = await _pizzaServices.GetStandartPizzaByIdAsync(id);

            return View(pizza);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Pizza pizza)
        {
            if (ModelState.IsValid)
            {
                var pizzasByName = _pizzaServices.GetPizzasByName(pizza.Name);

                IEnumerable<Pizza> anotherPizzaAlsoNamed = pizzasByName.Where(p => p.Id != pizza.Id) ?? new List<Pizza>();

                if (anotherPizzaAlsoNamed.Count() > 0)
                {
                    ModelState.AddModelError("", $"Pizza {pizza.Name} already exists");
                    return View(pizza);
                }

                var existingPizza = await _pizzaServices.GetStandartPizzaByIdAsync(id);

                if (pizza.ImageUpload != null)
                {
                    existingPizza.ImagePath = await _pizzaServices.AddNewPizzaImageAsync(pizza.ImageUpload);
                }

                existingPizza.Name = pizza.Name;
                existingPizza.Price = pizza.Price;

                TempData["Success"] = await _pizzaServices.UpdatePizzaInDataBaseAsync(existingPizza);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            Pizza pizza = await _pizzaServices.GetStandartPizzaByIdAsync(id);

            TempData["Success"] = await _pizzaServices.DeletePizzaFromDataBaseAsync(pizza);
            return RedirectToAction("Index");
        }
    }
}
