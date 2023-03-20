using PizzaWebAppAuthentication.Models.AppModels;
using System.ComponentModel.DataAnnotations;

namespace PizzaWebAppAuthentication.Models.ViewModels.PizzaViewModels
{
    public class PizzaViewModel
    {
        [Required(ErrorMessage ="Выберите основу для пиццы")]
        public string Base { get; set; }
        
        [Required(ErrorMessage = "Выберите размер пиццы")]
        public double Size { get; set; }

        [Required]
        public List<string> Ingredients { get; set; }
    }
}
