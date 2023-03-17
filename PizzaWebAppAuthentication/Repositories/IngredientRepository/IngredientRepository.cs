using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Repositories.IngredientRepository
{
    public class IngredientRepository:IIngredientRepository
    {
        private readonly ApplicationDbContext _context;
        public IngredientRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            return await _context.Ingredients.ToListAsync();
        }

        public async Task<IEnumerable<Ingredient>> GetIngredientsByName(string name) 
        {
            return await _context.Ingredients.Where(i => i.Name == name).ToListAsync();
        }

        public async Task<Ingredient> GetIngredientById(int id)
        {
            return await _context.Ingredients.Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<string> AddIngredientToDataBaseAsync(Ingredient ingredient)
        {
            _context.Add(ingredient);

            await _context.SaveChangesAsync();

            return $"Ingredient {ingredient.Name} has been created";
        }

        public async Task<bool> IngredientExistsAsync(string name, int id)
        {
            return await _context.Ingredients.AnyAsync(i => i.Name == name && i.Id != id);
        }

        public async Task<string> UpdateIngredientInDataBaseAsync(Ingredient ingredient)
        {
            _context.Update(ingredient);

            await _context.SaveChangesAsync();

            return $"Ingredient {ingredient.Name} has been edited";
        }

        public async Task<string> DeleteIngredientAsync(Ingredient ingredient)
        {
                _context.Ingredients.Remove(ingredient);
                await _context.SaveChangesAsync();
                return $"Ingredient {ingredient.Name} has been deleted";
        }

    }
}
