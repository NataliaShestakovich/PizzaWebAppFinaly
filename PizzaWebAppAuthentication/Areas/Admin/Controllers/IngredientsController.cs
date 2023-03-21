using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Options;
using PizzaWebAppAuthentication.Services.IngredientServices;

namespace PizzaWebAppAuthentication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "OnlyAdmin")]
    public class IngredientsController : Controller
    {
        private readonly IIngredientServises _ingredientServices;
        private readonly PizzaOption _pizzaOption;
        private readonly ILogger<IngredientsController> _logger;

        public IngredientsController(IIngredientServises ingredientServices,
                                     PizzaOption pizzaOption,
                                     ILogger<IngredientsController> logger)
        {
            _ingredientServices = ingredientServices;
            _pizzaOption = pizzaOption;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View((await _ingredientServices.GetIngredientsAsync()) ?? new List<Ingredient>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ingredient ingredient)
        {
            try
            {
                if (await _ingredientServices.IsExistIngredientAsync(ingredient))
                {
                    ModelState.AddModelError("", string.Format(_pizzaOption.IngredientDuplicationError, ingredient.Name));
                    return View(ingredient);
                }
                if (ModelState.IsValid)
                {
                    await _ingredientServices.AddIngredientToDataBaseAsync(ingredient);
                    TempData["Success"] = string.Format(_pizzaOption.SuccessAddIngredientToDatabase, ingredient.Name);
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = _pizzaOption.ErrorAddIngredientToDatabase;
                    return View(ingredient);
                }
            }
            catch (NullReferenceException)
            {
                CreateNotification();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                Ingredient ingredient = await _ingredientServices.GetIngredientById(id);
                return View(ingredient);
            }
            catch (NullReferenceException)
            {
                CreateNotification();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ingredient ingredient)
        {
            try
            {
                if (await _ingredientServices.IsUniqeNameAsync(id, ingredient))
                {
                    ModelState.AddModelError("", string.Format(_pizzaOption.IngredientDuplicationError, ingredient.Name));

                    return View(ingredient);
                }
                if (ModelState.IsValid)
                {
                    await _ingredientServices.UpdateIngredientInDataBaseAsync(ingredient);

                    TempData["Success"] = string.Format(_pizzaOption.SuccessUpdateIngredientInDatabase, ingredient.Name);

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = string.Format(_pizzaOption.ErrorUpdateIngredientInDatabase, ingredient.Name);

                    return View(ingredient);
                }
            }
            catch (NullReferenceException)
            {
                CreateNotification();
                return View(ingredient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Ingredient ingredient = await _ingredientServices.GetIngredientById(id);
                if (ingredient != null)
                {
                    await _ingredientServices.DeleteIngredientAsync(ingredient);

                    TempData["Success"] = string.Format(_pizzaOption.SuccessDeleteIngredientFromDatabase, ingredient.Name);                    
                }
                else
                {
                    TempData["Error"] = string.Format(_pizzaOption.ErrorDeleteIngredientFromDatabase, ingredient.Name);                    
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        private void CreateNotification()
        {
            var message = _pizzaOption.ErrorFindObject;
            TempData["Error"] = message;
            _logger.LogError(message);
        }
    }
}
