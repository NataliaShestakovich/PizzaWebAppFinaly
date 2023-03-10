using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles;

namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class Order
    {
        public Guid Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime OrderDate { get; set; }
      
        public ICollection<CartItemViewModel> OrderItems {get; set; }

        public virtual Address DeliveryAddress { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
