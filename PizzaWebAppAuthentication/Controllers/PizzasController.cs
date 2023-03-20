using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Infrastructure;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels.PizzaViewModels;
using PizzaWebAppAuthentication.Options;
using PizzaWebAppAuthentication.Services.PizzaServises;

namespace PizzaWebAppAuthentication.Controllers
{
    public class PizzasController : Controller
    {
        private readonly IPizzaServices _pizzaServices;
        private readonly PizzaOption _pizzaOption;

        public PizzasController(IPizzaServices pizzaservices, PizzaOption pizzaOption)
        {
            _pizzaServices = pizzaservices;
            _pizzaOption = pizzaOption;
        }

        [HttpGet]
        public async Task<IActionResult> CreateCustomPizza() 
        {
            var ingredients = await _pizzaServices.GetAvailableIngredientNames();
            ViewData["Ingredients"] = ingredients;

            var bases = await _pizzaServices.GetPizzaBases() ;
            ViewData["Bases"] = bases;

            var sizes = await _pizzaServices.GetSizes();
            ViewData["Sizes"] = sizes;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomPizza(PizzaViewModel pizza)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = _pizzaOption.ErrorCreatingPizza;
                return RedirectToAction("CreateCustomPizza");
            }

            var newPizza = new Pizza()
            {
                Id = Guid.NewGuid(),
                Standart = false,
                Name = _pizzaOption.CustomPizzaName,
                PizzaBase = await _pizzaServices.GetPizzaBaseByName(pizza.Base),
                Size = await _pizzaServices.GetSizeByDiameter(pizza.Size)
            };

            decimal ingredientCost = 0;
            foreach (var item in pizza.Ingredients)
            {
                var ingredient = await _pizzaServices.GetIngredientByName(item);
                newPizza.Ingredients.Add(ingredient);
                ingredientCost += ingredient.Price;
            }

            newPizza.Price = newPizza.PizzaBase.Price + (decimal)newPizza.Size.Diameter * 0.1m + ingredientCost;

            var customPizzas = HttpContext.Session.GetJson<List<Pizza>>("CustomPizza") ?? new List<Pizza>();
            customPizzas.Add(newPizza);

            HttpContext.Session.SetJson("CustomPizza", customPizzas);

            return RedirectToAction("AddPizza", "Cart", newPizza);
        }
    }
}
