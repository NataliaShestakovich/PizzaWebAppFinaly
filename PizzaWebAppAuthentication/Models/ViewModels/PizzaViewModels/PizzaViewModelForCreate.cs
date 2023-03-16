using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PizzaWebAppAuthentication.Infrastructure.Validation;

namespace PizzaWebAppAuthentication.Models.ViewModels.PizzaViewModels
{
    public class PizzaViewModelForAdmin
    {
        public List<string> Ingredients { get; set; }

        public string Name { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a value")]
        [Column(TypeName = "decimal(4, 2)")]
        public decimal Price { get; set; }
        
        [FileExtention]
        public IFormFile ImageUpload { get; set; }        
    }
}