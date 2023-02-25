using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Repositories
{
    public interface IPizzaRepository
    {
        public Task<IEnumerable<Pizza>> GetPizzas();
    }
}
