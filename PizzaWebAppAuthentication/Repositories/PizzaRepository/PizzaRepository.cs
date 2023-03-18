using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Repositories.PizzaRepository
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly ApplicationDbContext _context;

        public PizzaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pizza>> GetStandartPizzasAsync()
        {
            var pizzas = await _context.Pizzas
                .Where(c => c.Standart == true)
                .Include(c => c.Ingredients)
                .Include(c => c.PizzaBase)
                .Include(c => c.Size)
                .ToListAsync();

            return pizzas;
        }

        public async Task<Pizza> GetStandartPizzaByIdAsync(Guid id)
        {
            var pizza = await _context.Pizzas
                .Where(c => c.Standart == true)
                .Where(p => p.Id == id)
                .Include(c => c.Ingredients)
                .Include(c => c.PizzaBase)
                .Include(c => c.Size)
                .FirstOrDefaultAsync();

            return pizza;
        }

        public async Task<IEnumerable<string>> GetIngredientNames()
        {
            return await _context.Ingredients.Select(x => x.Name).ToListAsync();
        }

        public async Task<Ingredient> GetIngredientByName(string ingredientName)
        {
            return await _context.Ingredients.Where(c => c.Name == ingredientName).FirstOrDefaultAsync();
        }

        public async Task<PizzaBase> GetPizzaBaseByName(string baseName)
        {
            return await _context.Bases.Where(c => c.Name == baseName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<string>> GetPizzaBaseNames()
        {
            return await _context.Bases.Select(c => c.Name).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetSizeNames()
        {
            return await _context.Sizes.Select(c => c.Name).ToListAsync();
        }

        public async Task<Size> GetSizeByDiameter(double sizeName)
        {
            return await _context.Sizes.Where(c => c.Diameter == sizeName).FirstOrDefaultAsync();
        }

        public async Task<List<Pizza>> GetPizzasByName(string name)
        {
            var pizzas = await _context.Pizzas.Where(p => p.Name == name)
                                        .ToListAsync();
            return pizzas;
        }

        public async Task AddPizzaToDataBaseAsync(Pizza pizza)
        {
            _context.Add(pizza);

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePizzaInDataBaseAsync(Pizza pizza)
        {
            _context.Update(pizza);

            await _context.SaveChangesAsync();           
        }

        public async Task DeletePizzaFromDataBaseAsync(Pizza pizza)
        {
            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();            
        }
    }
}
