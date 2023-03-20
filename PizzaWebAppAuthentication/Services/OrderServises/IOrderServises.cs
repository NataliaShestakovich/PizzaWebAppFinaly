using PizzaWebAppAuthentication.Models.ViewModels.OrderViewModel;

namespace PizzaWebAppAuthentication.Services.OrderServices
{
    public interface IOrderServices
    {
        Task<IEnumerable<OrderViewModel>> GetOrderViewModel();
    }
}
