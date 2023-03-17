using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Repositories.IngredientRepository;
using PizzaWebAppAuthentication.Repositories.PizzaRepository;

namespace PizzaWebAppAuthentication.Services.IngredientServices
{
    public class IngredientServises: IIngredientServises
    {
        private readonly IIngredientRepository _iIngredientRepository;
       
        public IngredientServises (IIngredientRepository iIngredientRepository)
        {
            _iIngredientRepository = iIngredientRepository;           
        }
        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            return await _iIngredientRepository.GetIngredientsAsync();
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsByName(string name)
        {
            return await _iIngredientRepository.GetIngredientsByName(name);
        }

        public async Task<Ingredient> GetIngredientById(int id)
        {
            return await _iIngredientRepository.GetIngredientById(id);
        }

        public async Task<bool> IngredientExistsAsync(string name, int id)
        {
            return await _iIngredientRepository.IngredientExistsAsync (name, id);
        }

        public async Task<string> AddIngredientToDataBaseAsync(Ingredient ingredient)
        {
            return await _iIngredientRepository.AddIngredientToDataBaseAsync(ingredient);
        }

        public async Task<string> UpdateIngredientInDataBaseAsync(Ingredient ingredient)
        {
            return await _iIngredientRepository.UpdateIngredientInDataBaseAsync (ingredient);
        }
    }
}
