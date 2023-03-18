using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Infrastructure;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels.PizzaViewModels;

namespace PizzaWebAppAuthentication.Controllers
{
    public class PizzasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PizzasController(ApplicationDbContext context)
        {
            _context= context;
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
            newPizza.Standart = false;
            newPizza.Name = "Клиентская: ";

            newPizza.PizzaBase = _context.Bases.Where(c => c.Name == pizza.Base).FirstOrDefault();

            newPizza.Size = _context.Sizes.Where(c => c.Diameter == pizza.Size).FirstOrDefault();

            decimal ingredientCost = 0;
            int index = 1;

            foreach (var item in pizza.Ingredients)
            {
                var ingredient = _context.Ingredients.Where(c => c.Name == item).FirstOrDefault();
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
