using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels.OrderViewModel;
using PizzaWebAppAuthentication.Repositories.OrderRepository;
using PizzaWebAppAuthentication.Services.OrderServices;

namespace PizzaWebAppAuthentication.Services.OrderServices
{
    public class OrderServices : IOrderServices
    {
        private readonly IOrderRepository _orderRepository;
        
        public OrderServices(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;           
        }

        public async Task<IEnumerable<OrderViewModel>> GetOrderViewModel()
        {
            IEnumerable<Order> orders = await _orderRepository.GetOrders();

            var orderViewModels = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                foreach (var cartItem in order.CartItems)
                {
                    
                    OrderViewModel orderViewModel = new OrderViewModel()
                    {
                        OrderDate = order.OrderDate,
                        PizzaName = cartItem.Pizza.Name,
                        Price = cartItem.Pizza.Price,
                        Email = order.ApplicationUser.Email,
                        OrderId = order.Id
                    };

                    orderViewModels.Add(orderViewModel);
                }
            }
            return orderViewModels;
        }       
    }
}
