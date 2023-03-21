using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Repositories.IngredientRepository
{
    public interface IIngredientRepository
    {
        Task<IEnumerable<Ingredient>> GetAllAsync();
        Task<IEnumerable<Ingredient>> GetIngredientsByName(string name);
        Task<Ingredient> GetByIdAsync(int id);
        Task<bool> IngredientExistsAsync(string name, int id);

        Task CreateAsync(Ingredient ingredient);
        Task UpdateAsync(Ingredient ingredient);
        Task DeleteAsync(Ingredient ingredient);
    }
}
