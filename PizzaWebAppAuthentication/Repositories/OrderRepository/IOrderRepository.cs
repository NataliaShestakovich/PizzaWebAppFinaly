using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Repositories.OrderRepository;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrders();
}
