using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Repositories.RepositoryBase;

namespace PizzaWebAppAuthentication.Repositories
{
    public class UserRepository : RepositoryBase <ApplicationUser, int>
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) { }
        
    }
}
