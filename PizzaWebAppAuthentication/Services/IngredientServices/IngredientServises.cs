using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Repositories.IngredientRepository;

namespace PizzaWebAppAuthentication.Services.IngredientServices
{
    public class IngredientServises : IIngredientServises
    {
        private readonly IIngredientRepository _iIngredientRepository;

        public IngredientServises(IIngredientRepository iIngredientRepository)
        {
            _iIngredientRepository = iIngredientRepository;
        }
        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            return await _iIngredientRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsByName(string name)
        {
            return await _iIngredientRepository.GetIngredientsByName(name);
        }

        public async Task<Ingredient> GetIngredientById(int id)
        {
            return await _iIngredientRepository.GetByIdAsync(id);
        }

        public async Task<bool> IngredientExistsAsync(string name, int id)
        {
            return await _iIngredientRepository.IngredientExistsAsync(name, id);
        }

        public async Task AddIngredientToDataBaseAsync(Ingredient ingredient)
        {
            await _iIngredientRepository.CreateAsync(ingredient);
        }

        public async Task UpdateIngredientInDataBaseAsync(Ingredient ingredient)
        {
            await _iIngredientRepository.UpdateAsync(ingredient);
        }

        public async Task DeleteIngredientAsync(Ingredient ingredient)
        {
            await _iIngredientRepository.DeleteAsync(ingredient);
        }

        public async Task<bool> IsExistIngredientAsync(Ingredient ingredient)
        {
            var existingIngredients = await GetIngredientsByName(ingredient.Name);

            if (existingIngredients.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public async Task<bool> IsUniqeNameAsync(int id, Ingredient ingredient)
        {
            var existingOtherIngredientWithName = await IngredientExistsAsync(ingredient.Name, id);

            if (existingOtherIngredientWithName)
            {
                return true;
            }
            return false;
        }
    }
}
