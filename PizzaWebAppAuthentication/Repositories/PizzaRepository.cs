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
            _context= context;
        }
        
        public async Task<IEnumerable<Pizza>> GetPizzas()
        {
             
                var pizzas = await _context.Pizzas.ToListAsync();
            return pizzas;
        }
    }
}
