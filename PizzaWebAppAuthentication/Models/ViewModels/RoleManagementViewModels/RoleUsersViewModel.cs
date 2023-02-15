using Microsoft.AspNetCore.Mvc.Rendering;

namespace PizzaWebAppAuthentication.Models.ViewModels.RoleManagementViewModels
{
    public class RoleUsersViewModel
    {
        public List<SelectListItem> SelectListRole { get; set; }

        public string SelectedRole { get; set; }

        public List<string> Users { get; set; }
    }
}
