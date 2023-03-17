using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Services.IngredientServices;

namespace PizzaWebAppAuthentication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IngredientsController : Controller
    {
        private readonly IIngredientServises _ingredientServices;

        public IngredientsController(IIngredientServises ingredientServices)
        {
            _ingredientServices = ingredientServices;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _ingredientServices.GetIngredientsAsync());
        }

        public IActionResult Create(Guid id)
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ingredient ingredient)
        {

            var existingIngredients = await _ingredientServices.GetIngredientsByName(ingredient.Name);

            if (existingIngredients.Count() > 0)
            {
                ModelState.AddModelError("", $"Ingredient {ingredient.Name} already exists");
                return View(ingredient);
            }

            if (ModelState.IsValid)
            {
                TempData["Success"] = await _ingredientServices.AddIngredientToDataBaseAsync(ingredient);

                return RedirectToAction("Index");
            }

            return View(ingredient);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Ingredient ingredient = await _ingredientServices.GetIngredientById(id);

            return View(ingredient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ingredient ingredient)
        {
            var existingOtherIngredientWithName = await _ingredientServices.IngredientExistsAsync(ingredient.Name, id);

            if (existingOtherIngredientWithName)
            {
                ModelState.AddModelError("", $"Ingredient {ingredient.Name} already exists");
                return View(ingredient);
            }

            if (ModelState.IsValid)
            {
                TempData["Success"] = await _ingredientServices.UpdateIngredientInDataBaseAsync(ingredient);

                return RedirectToAction("Index");
            }

            return View(ingredient);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Ingredient ingredient = await _ingredientServices.GetIngredientById(id);

            if (ingredient != null)
            {
                TempData["Success"] = _ingredientServices.DeleteIngredientAsync(ingredient);
            }

            return RedirectToAction("Index");
        }
    }
}
