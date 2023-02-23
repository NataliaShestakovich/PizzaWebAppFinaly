using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Models.ViewModels.UserManagementViewModels
{
    public class UsersListViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public SelectList Role { get; set; }
        public SelectList FirstName { get; set; }

    }
}
