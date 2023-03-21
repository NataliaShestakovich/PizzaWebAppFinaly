using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Repositories.RepositoryBase;
using SendGrid.Helpers.Mail;

namespace PizzaWebAppAuthentication.Repositories.IngredientRepository
{
    public class FullIngredientRepository
    {
        //public class IngredientRepository : RepositoryBase<Ingredient, int>, IIngredientRepository
        //{
        //    public IngredientRepository(ApplicationDbContext dbContext) : base(dbContext)
        //    {
        //    }
                       
        //    public async Task<IEnumerable<Ingredient>> GetIngredientsByName(string name)
        //    {
        //        return await _dbContext.Set<Ingredient>().Where(i => i.Name == name).ToListAsync();
        //    }            

        //    public async Task<Ingredient> GetIngredientByName(string ingredientName)
        //    {
        //        return await _dbContext.Set<Ingredient>().Where(c => c.Name == ingredientName).FirstOrDefaultAsync();
        //    }           
            
        //    public async Task<IEnumerable<string>> GetIngredientNames()
        //    {
        //        return await _dbContext.Set<Ingredient>().Select(x => x.Name).ToListAsync();
        //    }

        //    public async Task<IEnumerable<string>> GetAvailableIngredientNames()
        //    {
        //        return await _dbContext.Set<Ingredient>().Where(x => x.Availability == true).Select(x => x.Name).ToListAsync();
        //    }
           
        //    public async Task<bool> IngredientExistsAsync(string name, int id)
        //    {
        //        return await _dbContext.Set<Ingredient>().AnyAsync(i => i.Name == name && i.Id != id);
        //    }
        //}
    }
}
