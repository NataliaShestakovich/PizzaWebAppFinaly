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

        public async Task<string> UpdatePizzaInDataBaseAsync(Pizza pizza)
        {
            return await _pizzaRepository.UpdatePizzaInDataBaseAsync(pizza);
        }

        public async Task<string> DeletePizzaFromDataBaseAsync(Pizza pizza)
        {
            return await _pizzaRepository.DeletePizzaFromDataBaseAsync(pizza);
        }

        public List<Pizza> GetPizzasByName(string name)
        {
            return _pizzaRepository.GetPizzasByName(name);
        }

    }
}
