using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles
{
    public class CartViewModel
    {
        public ICollection<CartItem> CartItems { get; set; }

        public decimal GrandTotal { get; set;}
    }
}
