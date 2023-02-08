using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Repositories
{
    public class UserRepository : IRepository<ApplicationUser, Guid>
    {
        public void Delete(ApplicationUser objectT)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid objectT)
        {
            throw new NotImplementedException();
        }

        public ApplicationUser? Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<ApplicationUser> GetList()
        {
            throw new NotImplementedException();
        }

        public void Update(ApplicationUser objectT)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid objectT)
        {
            throw new NotImplementedException();
        }
    }
}
