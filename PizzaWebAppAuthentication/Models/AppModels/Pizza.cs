using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Infrastructure.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class Pizza
    {
        public Pizza()
        {
            Ingredients = new List<Ingredient>();
        }
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Enter the name of the pizza")]
        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value")]
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }

        public PizzaBase PizzaBase { get; set; }

        public Size Size { get; set; } // можно установить дефолтное значение??

        [Required]
        public string ImagePath { get; set; }

        public List<Ingredient> Ingredients { get; set; }


        [Required, MinLength(10, ErrorMessage = "Minimum length is 10")]
        public string Description { get; set; }

        private string _composition;

        public string Composition {
            get 
            {
                if (string.IsNullOrEmpty(_composition))
                {
                    _composition = FormCompositionDefault();
                }
                return _composition;
            }
            set 
            {
                _composition = value;
            }
        } 

        [NotMapped]
        [FileExtention]
        public IFormFile ImageUpload { get; set; }    
        
        private string FormCompositionDefault()
        {
            string composition = string.Empty;
            var counter = 0;

            if (Ingredients != null || Ingredients.Count > 0)
            {
                foreach (var ingredient in Ingredients)
                {
                    counter++;

                    composition += ingredient.Name;

                    if (counter == Ingredients.Count)
                    {
                        composition += ", ";
                    }
                }
            } 
            return (composition);
        }

    }
}
