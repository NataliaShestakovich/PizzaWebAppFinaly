using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class Pizza
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Precision(25, 5)]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }

        public string Description { get; set; }

        public string Composition { get; set; }

        public virtual ICollection<Order> RelatedOrders  { get; set; } // заказы в которых присутствует эта пицца
    }
}
