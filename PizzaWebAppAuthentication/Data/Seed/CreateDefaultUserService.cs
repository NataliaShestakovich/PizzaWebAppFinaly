using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Data.Seed
{
    public class CreateDefaultUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolesManager;
        private readonly ILogger _logger;

        public CreateDefaultUserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
             ILoggerFactory loggerFactory
             )
        {
            _userManager = userManager;
            _rolesManager = roleManager;
            _logger = loggerFactory.CreateLogger<CreateDefaultUserService>();
        }

        public async Task CreateRoles()
        {
            var adminRoleName = "Admin";

            var rolesExist = await _rolesManager.Roles.FirstOrDefaultAsync<IdentityRole>(c => c.Name == adminRoleName);
            
            if (rolesExist == null)
            {
                var roleNames = new List<string> { adminRoleName, "User"}; // перенести в опшены перечень ролей

                foreach (var roleName in roleNames)
                {
                    bool role = await _rolesManager.RoleExistsAsync(roleName);
                    if (!role)
                    {
                        var result = await _rolesManager.CreateAsync(new IdentityRole(roleName));
                        
                        _logger.LogInformation("Create {0}: {1}", roleName, result.Succeeded);
                    }
                }
            }
            else
            {
                _logger.LogInformation("Roles seem to be already in place.");
            }

            //administrator
           var adminUserToBeCreated = new ApplicationUser()
           {
               FirstName = "Admin",
               LastName = "Administrator",
               UserName = "admin@pizza.com",
               Email = "admin@pizza.com",
               EmailConfirmed = true
           };

            var existingAdminUser = await _userManager.FindByEmailAsync(adminUserToBeCreated.Email);
            if (existingAdminUser == null)
            {
                var adminUser = await _userManager.CreateAsync(adminUserToBeCreated, "P@ssw0rd!");
                if (adminUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(adminUserToBeCreated, adminRoleName);
                    _logger.LogInformation("Created admin {0} and added to role {1}", adminUserToBeCreated.UserName, adminRoleName);
                }
            }
            else
            {
                _logger.LogInformation("Admin user is already there, making no changes to it!");
            }
        }
    }
}
