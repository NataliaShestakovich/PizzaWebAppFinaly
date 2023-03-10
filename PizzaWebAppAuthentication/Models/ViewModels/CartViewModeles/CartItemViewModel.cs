using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles
{
    public class CartItemViewModel
    {
        public Guid ID { get; set; }

        public Guid PizzaId { get; set; }

        public string PizzaName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get { return Quantity * Price; } }

        public string Image { get; set; }

        public CartItemViewModel() { }

        public CartItemViewModel(Pizza pizza)
        {
            PizzaId = pizza.Id;
            PizzaName = pizza.Name;
            Price = pizza.Price;
            Quantity = 1;
            Image = pizza.ImagePath;
        }
    }
}
