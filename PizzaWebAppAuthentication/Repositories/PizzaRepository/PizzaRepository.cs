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

        public async Task<IEnumerable<string>> GetIngredientNames() // ДОбавлен в новый репозиторий ингредиентов
        {
            return await _context.Ingredients.Select(x => x.Name).ToListAsync();
        }

        public async Task<IEnumerable<string>> GetAvailableIngredientNames() // ДОбавлен в новый репозиторий ингредиентов
        {   
            return await _context.Ingredients.Where(x => x.Availability == true).Select(x => x.Name).ToListAsync();
        }

        public async Task<Ingredient> GetIngredientByName(string ingredientName) //ДОбавлен в новый репозиторий ингредиентов
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

        public async Task<IEnumerable<PizzaBase>> GetPizzaBases()
        {
            return await _context.Bases.ToListAsync();
        }

        public async Task<IEnumerable<Size>> GetSizes()
        {
            return await _context.Sizes.ToListAsync();
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

        public async Task AddPizzaToDatabaseAsync(Pizza pizza)
        {
            _context.Add(pizza);

            await _context.SaveChangesAsync();
        }

        public async Task AddCustomPizzaToDatabaseAsync(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            _context.Entry(pizza.Size).State = EntityState.Unchanged;
            _context.Entry(pizza.PizzaBase).State = EntityState.Unchanged;
           
            foreach (var item in pizza.Ingredients)
            {
                _context.Entry(item).State = EntityState.Unchanged;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdatePizzaInDatabaseAsync(Pizza pizza)
        {
            _context.Update(pizza);

            await _context.SaveChangesAsync();           
        }

        public async Task DeletePizzaFromDatabaseAsync(Pizza pizza)
        {
            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();            
        }
    
        public async Task AddOrderToDatabase(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<Address> GetAddressAsync (Order order)
        {
            var address = await _context.Addresses.Where(c => c.City == order.DeliveryAddress.City).
                                                         Where(s => s.Street == order.DeliveryAddress.Street).
                                                         Where(b => b.Building == order.DeliveryAddress.Building).
                                                         Where(a => a.Apartment == order.DeliveryAddress.Apartment).
                                                         FirstOrDefaultAsync();
            return address;
        }
   
    }
}
