﻿using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Repositories.PizzaRepository;

public interface IPizzaRepository
{
    Task<IEnumerable<Pizza>> GetStandartPizzasAsync();

    Task<Pizza> GetStandartPizzaByIdAsync(Guid id);

    Task<IEnumerable<string>> GetIngredientNames();

    Task<Ingredient> GetIngredientByName(string ingredientName);

    Task<PizzaBase> GetPizzaBaseByName(string baseName);

    Task<IEnumerable<string>> GetPizzaBaseNames();

    Task<IEnumerable<string>> GetSizeNames();

    Task<Size> GetSizeByDiameter(double sizeName);

    Task<List<Pizza>> GetPizzasByName(string name);

    Task AddPizzaToDataBaseAsync(Pizza pizza);

    Task UpdatePizzaInDataBaseAsync(Pizza pizza);

    Task DeletePizzaFromDataBaseAsync(Pizza pizza);
}
