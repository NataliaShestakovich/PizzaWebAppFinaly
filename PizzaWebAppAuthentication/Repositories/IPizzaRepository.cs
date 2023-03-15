using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Repositories
{
    public interface IPizzaRepository
    {
        Task<IEnumerable<Pizza>> GetStandartPizzasAsync();

        IEnumerable<string> GetIngredients();

        PizzaBase GetPizzaBaseByName(string baseName);

        Size GetSizeByDiameter(double sizeName);

        Ingredient GetIngredientByName(string ingredientName);

        Task<string> AddPizzaToDataBaseAsync(Pizza pizza);
    }
}
