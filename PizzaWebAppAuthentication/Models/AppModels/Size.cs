using System.ComponentModel.DataAnnotations;

namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class Size
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double Diameter { get; set; }
    }
}
