using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class Order
    {
        public Guid Id { get; set; }

        public DateTime OrderDate { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }

        public virtual Address DeliveryAddress { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
