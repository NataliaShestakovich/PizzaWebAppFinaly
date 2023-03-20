using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PizzaWebAppAuthentication.Models.AppModels
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string LastName { get; set; }
        
        [Display(Name = "Login")]
        public override string? UserName { get => base.UserName; set => base.UserName = value; }

        public ICollection<Order> Orders { get; set; }
    }
}
