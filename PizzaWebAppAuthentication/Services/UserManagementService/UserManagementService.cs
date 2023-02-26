using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels.RoleManagementViewModels;
using System.Data;

namespace PizzaWebAppAuthentication.Services.RoleManagementService
{
    public class UserManagementService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public UserManagementService (RoleManager<IdentityRole> roleManager, 
                                      UserManager<ApplicationUser> userManager,
                                      ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public List <IdentityRole> GetRoles () // все роли
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<string> GetRoleByUserAsync(ApplicationUser user) // роль для пользователя
        {
            var role = await _userManager.GetRolesAsync(user);

            return role?.FirstOrDefault()??string.Empty;
        }

        public List<ApplicationUser> GetUsers() // всех юзеров
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

        public List<SelectListItem> GetSelectListRoles() // создание списка для выпадающего списка ролей
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

        public List<SelectListItem> GetSelectListEmail() // создание списка для выпадающего списка emails
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

        public List<SelectListItem> GetSelectListFirstName() // создание списка для выпадающего списка firstNames
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
            Task<bool> result = _roleManager.RoleExistsAsync(name);
            
            return result;
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

        public async Task<List<string>> GetUserNamesByRole(string name)
        {
            var users = new List<string>();
            var existedRole = await IsRoleExiste(name);

            if (!string.IsNullOrEmpty(name) && existedRole)
            {
                var role = _roleManager.Roles
                    .Where(r => r.Name == name)
                    .FirstOrDefault();

                if (role != null)
                {
                    var usersId = _context.UserRoles
                        .Where(r => r.RoleId == role!.Id)
                        .Select(u => u.UserId);

                    // перепроверить не может ли попасть сюда null
                    foreach (var id in usersId)
                    {
                        var emailUser = _userManager.Users
                            .Where(i => i.Id == id)
                            .Select(n => n.Email)
                            .FirstOrDefault();

                        users.Add(emailUser);
                    }
                }                
            }
            return users;
        }
    }
}
