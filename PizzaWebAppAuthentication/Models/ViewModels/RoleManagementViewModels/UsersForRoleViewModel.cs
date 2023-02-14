using Microsoft.AspNetCore.Mvc.Rendering;

namespace PizzaWebAppAuthentication.Models.ViewModels.RoleManagementViewModels
{
    public class UsersForRoleViewModel
    {
        public List<SelectListItem> RolesSelectList { get; set; }

        public string SelectedRole { get; set; }

        public List<string> UsersForSelectedRoles { get; set; }
    }
}
