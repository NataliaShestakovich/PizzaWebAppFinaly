using PizzaWebAppAuthentication.Data.Seed;

namespace PizzaWebAppAuthentication
{
    public class Startup
    {
        public static async Task InitializeIdentities(IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var identitySeeder = scope.ServiceProvider.GetService<CreateDefaultUserService>();
                await identitySeeder.CreateRoles();

                var initializerDb = scope.ServiceProvider.GetService<InitializeDataBase>();
                initializerDb.InitializePizzasTableDb();
                initializerDb.InitializeOrdersTableDb();
                initializerDb.InitializeUsersTableDb;
            }            
        }
    }
}
