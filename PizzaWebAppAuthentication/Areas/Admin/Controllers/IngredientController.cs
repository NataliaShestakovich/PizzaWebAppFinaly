using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Options;
using PizzaWebAppAuthentication.Services.IngredientServices;

namespace PizzaWebAppAuthentication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IngredientsController : Controller
    {
        private readonly IIngredientServises _ingredientServices;
        private readonly PizzaOption _pizzaOption;

        public IngredientsController(IIngredientServises ingredientServices, PizzaOption pizzaOption)
        {
            _ingredientServices = ingredientServices;
            _pizzaOption = pizzaOption;
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
                ModelState.AddModelError("", string.Format(_pizzaOption.IngredientDuplicationError, ingredient.Name));
                return View(ingredient);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ingredientServices.AddIngredientToDataBaseAsync(ingredient);

                    TempData["Success"] = string.Format(_pizzaOption.SuccessAddIngredientToDatabase, ingredient.Name);
                }
                catch (Exception)
                {

                    TempData["Error"] = string.Format(_pizzaOption.ErrorAddIngredientToDatabase, ingredient.Name);
                    return View(ingredient);
                }
                
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
                ModelState.AddModelError("", string.Format(_pizzaOption.IngredientDuplicationError, ingredient.Name));

                return View(ingredient);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _ingredientServices.UpdateIngredientInDataBaseAsync(ingredient);

                    TempData["Success"] = string.Format(_pizzaOption.SuccessUpdateIngredientInDatabase, ingredient.Name);
                }
                catch (Exception)
                {

                    TempData["Error"] = string.Format(_pizzaOption.ErrorUpdateIngredientInDatabase, ingredient.Name);
                    
                    return View(ingredient);
                }

                return RedirectToAction("Index");
            }

            return View(ingredient);
        }

        //[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Ingredient ingredient = await _ingredientServices.GetIngredientById(id);

            if (ingredient != null)
            {
                try
                {
                    await _ingredientServices.DeleteIngredientAsync(ingredient);

                    TempData["Success"] = string.Format(_pizzaOption.SuccessDeleteIngredientFromDatabase, ingredient.Name);
                }
                catch (Exception)
                {

                    TempData["Error"] = string.Format(_pizzaOption.ErrorDeleteIngredientFromDatabase, ingredient.Name);

                    return View(ingredient);
                }
            }

            return RedirectToAction("Index");
        }
    }
}
