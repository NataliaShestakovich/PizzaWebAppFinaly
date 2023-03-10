namespace PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles
{
    public class CartViewModel
    {
        public ICollection<CartItemViewModel> CartItems { get; set; }

        public decimal GrandTotal { get; set;}
    }
}
