using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Services.RoleManagementService
{
    public class RoleManagementService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public RoleManagementService (RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public List <IdentityRole> GetAllRoles ()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<IdentityResult> CreateAsync(string name)
        {
            if (!string.IsNullOrEmpty(name) && !await _roleManager.RoleExistsAsync(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                
                return result;
            }
            else
            {
                return IdentityResult.Failed();
            }
        }
    }
}
