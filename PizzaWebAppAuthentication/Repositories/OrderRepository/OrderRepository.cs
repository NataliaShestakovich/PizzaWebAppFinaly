using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Repositories.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            var orders = await _context.Orders
                .Include(c => c.CartItems)
                .ThenInclude(c => c.Pizza)
                .Include(c => c.ApplicationUser)
                .ToListAsync();
            
            return orders;
        }
    }
}
