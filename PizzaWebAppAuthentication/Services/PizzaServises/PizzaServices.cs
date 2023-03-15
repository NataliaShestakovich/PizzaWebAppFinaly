using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Repositories;

namespace PizzaWebAppAuthentication.Services.PizzaServises
{
    public class PizzaServices : IPizzaServices
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PizzaServices( IPizzaRepository pizzaRepository,
                              IWebHostEnvironment webHostEnvironment)
        {
            _pizzaRepository = pizzaRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IEnumerable<Pizza>> GetStandartPizzasAsync()
        {
            var pizzas = await _pizzaRepository.GetStandartPizzasAsync();

            return pizzas;
        }

        public IEnumerable<string> GetIngredients()
        {
            var ingredients = _pizzaRepository.GetIngredients();

            return ingredients;
        }

        public PizzaBase GetPizzaBaseByName(string baseName)
        {
            return (_pizzaRepository.GetPizzaBaseByName(baseName));
        }

       public Size GetSizeByDiameter(double sizeName)
        {
            return (_pizzaRepository.GetSizeByDiameter(sizeName));
        }

        public Ingredient GetIngredientByName(string ingredientName)
        {
            return (_pizzaRepository.GetIngredientByName(ingredientName));
        }

        public Task<string> AddPizzaToDataBaseAsync(Pizza pizza)
        {
            return (_pizzaRepository.AddPizzaToDataBaseAsync(pizza));
        }
    }
}
