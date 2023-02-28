using Microsoft.EntityFrameworkCore;

namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class Order
    {
        public Guid Id { get; set; }

        public virtual ApplicationUser User { get; set; }

        public DateTime OrderDate { get; set; }
      
        public virtual ICollection<Pizza> OrderItems {get; set; }

        public decimal TotalPrice { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public virtual Address DeliveryAddress { get; set; }
    }
}
