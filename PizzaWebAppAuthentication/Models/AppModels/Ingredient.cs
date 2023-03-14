using System.ComponentModel.DataAnnotations;

namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class Ingredient
    {
        public Ingredient()
        {
            Pizzas = new List<Pizza>();
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int PortionGrams { get; set; }

        public bool Availability { get; set; }

        public ICollection<Pizza> Pizzas { get; set; }
    }
}
