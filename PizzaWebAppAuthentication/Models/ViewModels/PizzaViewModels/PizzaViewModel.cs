using PizzaWebAppAuthentication.Models.AppModels;
using System.ComponentModel.DataAnnotations;

namespace PizzaWebAppAuthentication.Models.ViewModels.PizzaViewModels
{
    public class PizzaViewModel
    {
        [Required]
        public string Base { get; set; }
        
        [Required]
        [Range(1, double.MaxValue)]
        public double Size { get; set; }

        [Required]
        public List<string> Ingredients { get; set; }
    }
}
