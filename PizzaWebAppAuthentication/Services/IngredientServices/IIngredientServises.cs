using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Services.IngredientServices
{
    public interface IIngredientServises
    {
        Task<IEnumerable<Ingredient>> GetIngredientsAsync();
        Task<IEnumerable<Ingredient>> GetIngredientsByName(string name);
        Task<Ingredient> GetIngredientById(int id);
        Task<bool> IngredientExistsAsync(string name, int id);
        Task AddIngredientToDataBaseAsync(Ingredient ingredient);
        Task UpdateIngredientInDataBaseAsync(Ingredient ingredient);
        Task DeleteIngredientAsync(Ingredient ingredient);
    }
}
