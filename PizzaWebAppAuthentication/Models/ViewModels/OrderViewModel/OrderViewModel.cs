namespace PizzaWebAppAuthentication.Models.ViewModels.OrderViewModel
{
    public class OrderViewModel
    {
        public DateTime OrderDate { get; set; }
        public string PizzaName { get; set; }
        public decimal Price { get; set; }
        public string Email { get; set; }
        public Guid OrderId { get; set; }
    }
}
