using System.Data;

namespace PizzaWebAppAuthentication.Models.ViewModels.UserManagementViewModels
{
    public class UserDataViewModel
    {
        public Guid? Id { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Role { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public DateTime? DateLastOrder { get; set; }
    }
}
