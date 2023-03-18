using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Repositories.PizzaRepository;

namespace PizzaWebAppAuthentication.Services.PizzaServises
{
    public class PizzaServices : IPizzaServices
    {
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PizzaServices(IPizzaRepository pizzaRepository,
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

        public async Task<Pizza> GetStandartPizzaByIdAsync(Guid id)
        {
            var pizza = await _pizzaRepository.GetStandartPizzaByIdAsync(id);

            return pizza;
        }

        public async Task< IEnumerable<string>> GetIngredientNames()
        {
            var ingredients = await _pizzaRepository.GetIngredientNames();

            return ingredients;
        }

        public async Task<PizzaBase> GetPizzaBaseByName(string baseName)
        {
            return await _pizzaRepository.GetPizzaBaseByName(baseName);
        }

        public async Task<IEnumerable<string>> GetPizzaBaseNames()
        {
            return await _pizzaRepository.GetPizzaBaseNames();
        }

        public async Task<IEnumerable<string>> GetSizeNames()
        {
            return await _pizzaRepository.GetSizeNames();
        }

        public async Task<Size> GetSizeByDiameter(double sizeName)
        {
            return await _pizzaRepository.GetSizeByDiameter(sizeName);
        }

        public async Task<Ingredient> GetIngredientByName(string ingredientName)
        {
            return await _pizzaRepository.GetIngredientByName(ingredientName);
        }

        public async Task AddPizzaToDataBaseAsync(Pizza pizza)
        {
            await _pizzaRepository.AddPizzaToDataBaseAsync(pizza);
        }

        public async Task<string> AddNewPizzaImageAsync(IFormFile imageUpload)
        {
            string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "images");
            string imageName = Guid.NewGuid().ToString() + "_" + imageUpload.FileName;

            string filePath = Path.Combine(uploadsDir, imageName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageUpload.CopyToAsync(fileStream);
            }

            return imageName;
        }

        public async Task UpdatePizzaInDataBaseAsync(Pizza pizza)
        {
            await _pizzaRepository.UpdatePizzaInDataBaseAsync(pizza);
        }

        public async Task DeletePizzaFromDataBaseAsync(Pizza pizza)
        {
            await _pizzaRepository.DeletePizzaFromDataBaseAsync(pizza);
        }

        public async Task<List<Pizza>> GetPizzasByName(string name)
        {
            return await _pizzaRepository.GetPizzasByName(name);
        }

    }
}
