using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required(ErrorMessage ="Необходимо указать цену")]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(4, 2)")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Необходимо указать вес порции")]
        public int PortionGrams { get; set; }

        public bool Availability { get; set; }

        public ICollection<Pizza> Pizzas { get; set; }
    }
}
