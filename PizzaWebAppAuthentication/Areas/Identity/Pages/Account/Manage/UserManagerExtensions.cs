using Microsoft.AspNetCore.Identity;
using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Areas.Identity.Pages.Account.Manage
{
    public static class UserManagerExtensions
    {   
        public static async Task<IdentityResult> SetFirstNameAsync(this UserManager<ApplicationUser> userManager, ApplicationUser user, string firstName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.FirstName = firstName;
            var result = await userManager.UpdateAsync(user);

            return result;
        }

        public static async Task<IdentityResult> SetLastNameAsync(this UserManager<ApplicationUser> userManager, ApplicationUser user, string lastName)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.LastName = lastName;
            var result = await userManager.UpdateAsync(user);

            return result;
        }        
    }
}
