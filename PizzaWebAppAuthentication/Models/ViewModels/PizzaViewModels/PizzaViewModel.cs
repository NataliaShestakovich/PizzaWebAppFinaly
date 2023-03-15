using PizzaWebAppAuthentication.Models.AppModels;
using System.ComponentModel.DataAnnotations;

namespace PizzaWebAppAuthentication.Models.ViewModels.PizzaViewModels
{
    public class PizzaViewModel
    {
        public string Base { get; set; }
        public double Size { get; set; }

        [Required]
        public List<string> Ingredients { get; set; }
    }
}
