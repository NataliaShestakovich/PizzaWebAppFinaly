using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Infrastructure;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels.PizzaViewModels;
using PizzaWebAppAuthentication.Services.PizzaServises;

namespace PizzaWebAppAuthentication.Controllers
{
    public class PizzasController : Controller
    {
        private readonly IPizzaServices _pizzaServices;

        public PizzasController(IPizzaServices pizzaservices)
        {
            _pizzaServices = pizzaservices;
        }

        [HttpGet]
        public async Task<IActionResult> CreateCustomPizza() 
        {
            var ingredients = await _pizzaServices.GetIngredientNames();
            ViewData["Ingredients"] = ingredients;

            var bases = await _pizzaServices.GetPizzaBaseNames() ;
            ViewData["Bases"] = bases;

            var sizes = await _pizzaServices.GetSizeNames();
            ViewData["Sizes"] = sizes;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomPizzaAsync(PizzaViewModel pizza)
        {
            var newPizza = new Pizza();
            newPizza.Id = Guid.NewGuid();
            newPizza.Standart = false;
            newPizza.Name = "Клиентская: ";

            newPizza.PizzaBase = await _pizzaServices.GetPizzaBaseByName(pizza.Base);

            newPizza.Size = await _pizzaServices.GetSizeByDiameter(pizza.Size);

            decimal ingredientCost = 0;
            int index = 1;

            foreach (var item in pizza.Ingredients)
            {
                var ingredient = await _pizzaServices.GetIngredientByName(item);
                newPizza.Ingredients.Add(ingredient);
                ingredientCost += ingredient.Price;
                if (index < pizza.Ingredients.Count())
                {
                    newPizza.Name += $"{ingredient.Name}, ";
                }
                else
                {
                    newPizza.Name += $"{ingredient.Name}.";
                }
                index++;
            }

            newPizza.Price = newPizza.PizzaBase.Price + (decimal)newPizza.Size.Diameter * 0.1m + ingredientCost;

            var customPizzas = HttpContext.Session.GetJson<List<Pizza>>("CustomPizza") ?? new List<Pizza>();

            customPizzas.Add(newPizza);

            HttpContext.Session.SetJson("CustomPizza", customPizzas);

            return RedirectToAction("AddPizza", "Cart", newPizza);
        }
    }
}
