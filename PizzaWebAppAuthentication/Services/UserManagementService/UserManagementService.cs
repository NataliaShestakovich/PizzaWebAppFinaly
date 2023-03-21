using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;
using System.Data;

namespace PizzaWebAppAuthentication.Services.RoleManagementService
{
    public class UserManagementService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public UserManagementService (RoleManager<IdentityRole> roleManager, 
                                      UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public List <IdentityRole> GetRoles () 
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<string> GetRoleByUserAsync(ApplicationUser user) 
        {
            var role = await _userManager.GetRolesAsync(user);

            return role?.FirstOrDefault()??string.Empty;
        }

        public List<ApplicationUser> GetUsers() 
        {
            return _userManager.Users?.ToList()??new List<ApplicationUser>();
        }

        public async Task<List<ApplicationUser>> GetUsersByRoleAsync(string roleName) 
        {
            var role = await _roleManager?.FindByNameAsync(roleName);

            var users = await _userManager?.GetUsersInRoleAsync(role.Name);

            return users?.ToList()??new List<ApplicationUser>();
        }

        public async Task<ApplicationUser> GetUserByIDAsync(Guid id)
        {
            var users = await _userManager?.FindByIdAsync($"{id}")??new ApplicationUser();

            return users;
        }

        public List<SelectListItem> GetSelectListRoles() 
        {
            var selectListRole = new List<SelectListItem>();
            var roles = _roleManager.Roles.ToList();
            
            if (roles != null)
            {
                foreach (var role in roles)
                {
                    selectListRole.Add(new SelectListItem { Text = role.Name, Value = role.Name });
                }
            }
            return selectListRole;
        }

        public List<SelectListItem> GetSelectListEmail() 
        {
            var selectListEmail = new List<SelectListItem>();
            var emails = _userManager.Users.Select(p => p.Email).ToList();

            if (emails != null)
            {
                foreach (var email in emails)
                {
                    selectListEmail.Add(new SelectListItem { Text = email, Value = email});
                }
            }
            return selectListEmail;
        }

        public List<SelectListItem> GetSelectListFirstName() 
        {
            var selectListFirstName = new List<SelectListItem>();
            var firstNames = _userManager.Users.Select(p => p.FirstName).ToList();

            if (firstNames != null)
            {
                foreach (var name in firstNames)
                {
                    selectListFirstName.Add(new SelectListItem { Text = name, Value = name});
                }
            }
            return selectListFirstName;
        }

        public async Task<IdentityResult> CreateRoleAsync(string name)
        {
            if (!string.IsNullOrEmpty(name) && !await IsRoleExisteAsync(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                
                return result;
            }
            else
            {
                return IdentityResult.Failed();
            }
        }

        public Task<bool> IsRoleExisteAsync(string name)
        {
            Task<bool> result = _roleManager.RoleExistsAsync(name);
            
            return result;
        }

        public async Task<IdentityResult> Delete (string name)
        {
            if (!string.IsNullOrEmpty(name) && await IsRoleExisteAsync(name) && name != "Admin" && name != "User")
            {
                var role = await _roleManager.FindByNameAsync(name);
                var isExistUser = await GetUsersByRoleAsync(name);

                if (role != null && !isExistUser.Any())
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
