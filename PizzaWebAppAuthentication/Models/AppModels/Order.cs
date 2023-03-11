using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles;

namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid ApplicationUserId { get; set; }
        
        public DateTime OrderDate { get; set; }
      
        public virtual ICollection<CartItem> CartItems {get; set; }

        public virtual Address DeliveryAddress { get; set; }

        public decimal TotalPrice { get; set; }

        public ApplicationUser User { get; set; }
    }
}
