namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class CartItem
    {
        public long Id { get; set; }

        public long PizzaId { get; set; }

        public string PizzaName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
        
        public decimal TotalPrice { get { return Quantity * Price; }}

        public string Image { get; set; }

        public CartItem() { }

        public CartItem(Pizza pizza)
        {
            PizzaId = pizza.Id;
            PizzaName = pizza.Name;
            Price = pizza.Price;
            Quantity = 1;
            Image = pizza.ImageUrl;
        }
    }
}
