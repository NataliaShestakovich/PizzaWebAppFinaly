using Microsoft.EntityFrameworkCore;

namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class Order
    {
        public Guid Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime OrderDate { get; set; }
      
        public ICollection<CartItem> OrderItems {get; set; }

        public virtual Address DeliveryAddress { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
