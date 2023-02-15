using Microsoft.AspNetCore.Identity;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Services.RoleManagementService
{
    public class RoleManagementService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public RoleManagementService (RoleManager<IdentityRole> roleManager, 
                                      UserManager<ApplicationUser> userManager,
                                      ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
        }

        public List <IdentityRole> GetRoles ()
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

        public async Task<List<string>> GetUsersByRole(string name)
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
