using Microsoft.Build.Framework;

namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class OrderStatus
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
