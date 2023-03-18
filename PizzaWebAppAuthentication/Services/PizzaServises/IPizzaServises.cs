﻿using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Services.PizzaServises
{
    public interface IPizzaServices
    {
        Task<IEnumerable<Pizza>> GetStandartPizzasAsync();

        Task<Pizza> GetStandartPizzaByIdAsync(Guid id);

        Task<IEnumerable<string>> GetIngredientNames();

        Task<PizzaBase> GetPizzaBaseByName(string baseName);

        Task<IEnumerable<string>> GetPizzaBaseNames();

        Task<IEnumerable<PizzaBase>> GetPizzaBases();

        Task<IEnumerable<Size>> GetSizes();

        Task<IEnumerable<string>> GetSizeNames();

        Task<Size> GetSizeByDiameter(double sizeName);

        Task<Ingredient> GetIngredientByName(string ingredientName);
        
        Task<List<Pizza>> GetPizzasByName(string name);

        Task AddPizzaToDatabaseAsync(Pizza pizza);

        Task AddCustomPizzaToDatabaseAsync(Pizza pizza);

        Task<string> AddNewPizzaImageAsync(IFormFile imageUpload); 

        Task UpdatePizzaInDatabaseAsync(Pizza pizza);

        Task DeletePizzaFromDatabaseAsync(Pizza pizza);

        Task AddOrderToDatabase(Order order);

        Task<Address> GetAddressAsync(Order order);
    }
}
