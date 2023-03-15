using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Models.ViewModels.PizzaViewModels
{
    public class PizzaViewModel
    {
        public string Base { get; set; }
        public double Size { get; set; }
        public List<string> Ingredients { get; set; }
    }
}
