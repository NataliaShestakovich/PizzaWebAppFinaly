using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Options;
using PizzaWebAppAuthentication.Repositories.IngredientRepository;
using static PizzaWebAppAuthentication.Services.IngredientServices.FullIngredientServices;

namespace PizzaWebAppAuthentication.Services.IngredientServices
{
    public class FullIngredientServices
    {
        //    private readonly IFullIngredientRepository _iFullIngredientRepository;

        //    public IngredientServises(IFullIngredientRepository iFullIngredientRepository)
        //    {
        //    _iFullIngredientRepository = iFullIngredientRepository;
        //    }
        //    public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        //    {
        //        return await _iFullIngredientRepository.GetAllAsync();
        //    }

        //    public async Task<IEnumerable<Ingredient>> GetIngredientsByName(string name)
        //    {
        //        return await _iFullIngredientRepository.GetIngredientsByName(name);
        //    }

        //    public async Task<Ingredient> GetIngredientById(int id)
        //    {
        //        return await _iFullIngredientRepository.GetByIdAsync(id);
        //    }

        //    public async Task<bool> IngredientExistsAsync(string name, int id)
        //    {
        //        return await _iFullIngredientRepository.IngredientExistsAsync(name, id);
        //    }

        //    public async Task AddIngredientToDataBaseAsync(Ingredient ingredient)
        //    {
        //        await _iFullIngredientRepository.CreateAsync(ingredient);
        //    }

        //    public async Task UpdateIngredientInDataBaseAsync(Ingredient ingredient)
        //    {
        //        await _iFullIngredientRepository.UpdateAsync(ingredient);
        //    }

        //    public async Task DeleteIngredientAsync(Ingredient ingredient)
        //    {
        //        await _iFullIngredientRepository.DeleteAsync(ingredient);
        //    }

        //// Этот метод для начала метода Create POST
        //    public async Task<bool> IsExistIngredientAsync(Ingredient ingredient)
        //    {
        //        var existingIngredients = await GetIngredientsByName(ingredient.Name);

        //        if (existingIngredients.Any())
        //        {
        //            return true;                    
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //}
        //// к этому методу 
        //   try 
        //{	        
        // if(_fullIngredientServices.IsExistIngredientAsync(ingredient))
        //  {
        // ModelState.AddModelError("", string.Format(_pizzaOption.IngredientDuplicationError, ingredient.Name));
        //    return View(ingredient);
        //    }
        // }
        // catch (Exception)
        //{
        //_logger.LogError(ex, "Ошибка получения запрошенных данных из базы данных");
        //return RedirectToAction("Index")
        ////}
    }
}
