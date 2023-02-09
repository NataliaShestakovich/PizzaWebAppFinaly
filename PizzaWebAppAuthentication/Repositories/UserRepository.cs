using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Repositories
{
    public class UserRepository<T2> : IRepository <ApplicationUser, T2>
    {
        private readonly ApplicationDbContext _userContext;

        public UserRepository(ApplicationDbContext userContext)
        {
            _userContext = userContext;
        }

        public ApplicationUser? Get(T2 fieldName)
        {
            if (fieldName != null)
            {
                return _userContext.Users.Find(fieldName);
            }

            return null;
        }

        public List<ApplicationUser> GetList()
        {
            return _userContext.Users.ToList();
        }

        public void Update(ApplicationUser user)
        {
            _userContext.Update(user);

            _userContext.SaveChanges();
        }

        public void Delete(ApplicationUser user)
        {
            var specificUser = _userContext.Users.Find(user.Id);

            if (specificUser != null)
            {
                _userContext.Users.Remove(specificUser);

                _userContext.SaveChanges();
            }
        }

        public void Add(ApplicationUser user) // стремный метод, но он нужен для того чтобы интерфейс был универсальным
        {
            throw new NotSupportedException();
        }
    }
}
