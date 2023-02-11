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
            if (!string.IsNullOrEmpty(name) && !await IsRoleExiste(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                
                return result;
            }
            else
            {
                return IdentityResult.Failed();
            }
        }

        public Task<bool> IsRoleExiste(string name)
        {
            return _roleManager.RoleExistsAsync(name);
        }

        public async Task<IdentityResult> Delete (string name)
        {
            if (!string.IsNullOrEmpty(name) && await IsRoleExiste(name))
            {
                IdentityRole? role = await _roleManager.FindByNameAsync(name);
                if (role != null)
                {
                    IdentityResult result = await _roleManager.DeleteAsync(role);
                    return result;
                }
                return IdentityResult.Failed();
            }
            else
            {
                return IdentityResult.Failed();
            }
        }

    }
}
