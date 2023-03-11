using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Infrastructure.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class Pizza
    {
        public Pizza()
        {
            Ingredients = new List<Ingredient>();
        }
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Precision(25, 5)]
        public decimal Price { get; set; }

        public bool IsCustom { get; set; }

        public PizzaBase PizzaBase { get; set; }

        public Size Size { get; set; }

        public string ImagePath { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public string Description { get; set; }

        public string Composition { get; set; }

        [NotMapped]
        [FileExtention]
        public IFormFile ImageUpload { get; set; }        
    }
}
