using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Repositories.PizzaRepository;

public interface IPizzaRepository
{
    Task<IEnumerable<Pizza>> GetStandartPizzasAsync();

    Task<Pizza> GetStandartPizzaByIdAsync(Guid id);

    IEnumerable<string> GetIngredients();

    PizzaBase GetPizzaBaseByName(string baseName);

    Size GetSizeByDiameter(double sizeName);

    Ingredient GetIngredientByName(string ingredientName);

    List<Pizza> GetPizzasByName(string name);

    Task<string> AddPizzaToDataBaseAsync(Pizza pizza);

    Task<string> UpdatePizzaInDataBaseAsync(Pizza pizza);

    Task<string> DeletePizzaFromDataBaseAsync(Pizza pizza);
}
