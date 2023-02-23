using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Models.ViewModels.UserManagementViewModels
{
    public class UsersListViewModel
    {
        public List<ApplicationUser> Users { get; set; }
        public List<SelectListItem> Role { get; set; }
        public List<SelectListItem> FirstName { get; set; }
        public List<SelectListItem> Email { get; set; }
        public string SelectedRole { get; set; }
        public string SelectedFirstName { get; set; }
        public string SelectedEmail { get; set; }


    }
}
