using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles
{
    public class CartViewModel
    {
        public Dictionary<Pizza, int> Items { get; set; }

        public ApplicationUser User { get; set; }

        public decimal TotalPrice => Items.Sum(x => x.Key.Price * x.Value);
    }
}
