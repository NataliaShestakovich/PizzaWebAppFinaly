using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;
using System.Collections;

namespace PizzaWebAppAuthentication.Repositories
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

        public IEnumerable<string> GetIngredients()
        {
            return (_context.Ingredients.Select(x => x.Name).ToList());
        }

        public PizzaBase GetPizzaBaseByName(string baseName)
        {
            return (_context.Bases.Where(c => c.Name == baseName).FirstOrDefault());
        }

        public Size GetSizeByDiameter(double sizeName)
        {
            return (_context.Sizes.Where(c => c.Diameter == sizeName).FirstOrDefault());
        }

        public Ingredient GetIngredientByName(string ingredientName)
        {
            return (_context.Ingredients.Where(c => c.Name == ingredientName).FirstOrDefault());
        }

        public async Task<string> AddPizzaToDataBaseAsync(Pizza pizza)
        {
            string result = string.Empty;

            _context.Add(pizza);

            await _context.SaveChangesAsync();

            result = $"Pizza {pizza.Name} has been created";

            return result;
        }

    }
}
