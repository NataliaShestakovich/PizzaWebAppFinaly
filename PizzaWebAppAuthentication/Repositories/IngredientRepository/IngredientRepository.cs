using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Repositories.RepositoryBase;

namespace PizzaWebAppAuthentication.Repositories.IngredientRepository
{
    public class IngredientRepository: RepositoryBase<Ingredient, int>, IIngredientRepository
    {
        public IngredientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
       
        public async Task<IEnumerable<Ingredient>> GetIngredientsByName(string name) 
        {
            return await _dbContext.Set<Ingredient>().Where(i => i.Name == name).ToListAsync();
        }
        
        public async Task<bool> IngredientExistsAsync(string name, int id)
        {
            return await _dbContext.Set<Ingredient>().AnyAsync(i => i.Name == name && i.Id != id);
        }


        

        
        
        //public async Task<IEnumerable<Ingredient>> GetAllAsync()
        //{
        //    return await _dbContext.Ingredients.ToListAsync();
        //}
        //public async Task<Ingredient> GetByIdAsync(int id)
        //{
        //    return await _dbContext.Ingredients.Where(i => i.Id == id).FirstOrDefaultAsync();
        //}

        //public async Task CreateAsync(Ingredient ingredient)
        //{
        //    _dbContext.Add(ingredient);

        //    await _dbContext.SaveChangesAsync();
        //}


        //public async Task UpdateAsync(Ingredient ingredient)
        //{
        //    _dbContext.Update(ingredient);

        //    await _dbContext.SaveChangesAsync();
        //}

        //public async Task DeleteAsync(Ingredient ingredient)
        //{
        //        _dbContext.Ingredients.Remove(ingredient);
        //        await _dbContext.SaveChangesAsync();
        //}

    }
}
