using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels.PizzaViewModels;
using PizzaWebAppAuthentication.Options;
using PizzaWebAppAuthentication.Services.PizzaServises;

namespace PizzaWebAppAuthentication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "OnlyAdmin")]
    public class PizzaController : Controller
    {
        private readonly IPizzaServices _pizzaServices;
        private readonly PizzaOption _pizzaOption;

        public PizzaController(IPizzaServices pizzaservices, PizzaOption pizzaOption)
        {
            _pizzaServices = pizzaservices;
            _pizzaOption = pizzaOption;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pizzaServices.GetStandartPizzasAsync());
        }

        public async Task<IActionResult> Create()
        {
            var ingredients = await _pizzaServices.GetIngredientNames();
            ViewData["Ingredients"] = ingredients;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PizzaViewModelForAdmin pizzaViewModel)
        {
            var ingredients = await _pizzaServices.GetIngredientNames();
            ViewData["Ingredients"] = ingredients;
            Pizza newPizza = new Pizza();

            var existingPizzas = await _pizzaServices.GetPizzasByName(pizzaViewModel.Name);

            if (existingPizzas.Count > 0)
            {
                ModelState.AddModelError("", string.Format(_pizzaOption.ErrorAddInDatabase, pizzaViewModel.Name));
                return View(pizzaViewModel);
            }

            newPizza.Name = pizzaViewModel.Name;
            newPizza.Price = pizzaViewModel.Price;
            newPizza.Standart = true;
            newPizza.PizzaBase = await _pizzaServices.GetPizzaBaseByName(_pizzaOption.StandartPizzaBase);
            newPizza.Size = await _pizzaServices.GetSizeByDiameter(32);
            newPizza.Ingredients = new List<Ingredient>();

            if (ModelState.IsValid)
            {
                if (pizzaViewModel.ImageUpload != null)
                {
                    newPizza.ImagePath = await _pizzaServices.AddNewPizzaImageAsync(pizzaViewModel.ImageUpload);
                }

                if (pizzaViewModel.Ingredients == null || pizzaViewModel.Ingredients.Count == 0)
                {
                    TempData["Error"] = _pizzaOption.ErrorCreatingPizza;

                    return View(pizzaViewModel);
                }

                foreach (var item in pizzaViewModel.Ingredients)
                {
                    Ingredient ingredient = await _pizzaServices.GetIngredientByName(item);
                    newPizza.Ingredients.Add(ingredient);
                }

                try
                {
                    await _pizzaServices.AddPizzaToDatabaseAsync(newPizza);
                    TempData["Success"] = String.Format(_pizzaOption.SuccessAddPizzaToDatabase, newPizza.Name);
                }
                catch (Exception)
                {
                    TempData["Error"] = String.Format(_pizzaOption.SuccessAddPizzaToDatabase, newPizza.Name);
                }

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
                var pizzasByName = await _pizzaServices.GetPizzasByName(pizza.Name);

                IEnumerable<Pizza> anotherPizzaAlsoNamed = pizzasByName.Where(p => p.Id != pizza.Id) ?? new List<Pizza>();

                if (anotherPizzaAlsoNamed.Count() > 0)
                {
                    ModelState.AddModelError("", string.Format(_pizzaOption.ErrorAddInDatabase, pizza.Name));
                    return View(pizza);
                }

                var existingPizza = await _pizzaServices.GetStandartPizzaByIdAsync(id);

                if (pizza.ImageUpload != null)
                {
                    existingPizza.ImagePath = await _pizzaServices.AddNewPizzaImageAsync(pizza.ImageUpload);
                }

                existingPizza.Name = pizza.Name;
                existingPizza.Price = pizza.Price;

                try
                {
                    await _pizzaServices.UpdatePizzaInDatabaseAsync(existingPizza);
                    TempData["Success"] = String.Format(_pizzaOption.SuccessUpdatePizzaInDatabase, existingPizza.Name);
                }
                catch (Exception)
                {
                    TempData["Error"] = String.Format(_pizzaOption.ErrorUpdatePizzaInDatabase, existingPizza.Name);
                }
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            Pizza pizza = await _pizzaServices.GetStandartPizzaByIdAsync(id);

            try
            {
                await _pizzaServices.DeletePizzaFromDatabaseAsync(pizza);

                TempData["Success"] = String.Format(_pizzaOption.SuccessDeletePizzaFromDatabase, pizza.Name);
            }
            catch (Exception)
            {
                TempData["Error"] = String.Format(_pizzaOption.ErrorDeletePizzaFromDatabase, pizza.Name);
            }

            return RedirectToAction("Index");
        }
    }
}
