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

        public async Task<IEnumerable<Pizza>> GetStandartPizzas()
        {
            var pizzas = await _context.Pizzas
                .Where(c => c.Standart)
                .Include(c => c.Ingredients)
                .Include(c => c.PizzaBase)
                .Include(c => c.Size)
                .ToListAsync();
            return pizzas;
        }
    }
}
