namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class Address
    {
        public Guid Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public uint Building { get; set; }

        public int Apartment { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}