using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels;
using PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles;
using PizzaWebAppAuthentication.Models.ViewModels.PizzaViewModels;
using PizzaWebAppAuthentication.Repositories;
using PizzaWebAppAuthentication.Services.PizzaServises;
using SendGrid.Helpers.Mail;

namespace PizzaWebAppAuthentication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PizzaController: Controller
    {
        private readonly IPizzaServices _pizzaServices;
        private readonly ApplicationDbContext _contextDb;
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PizzaController(IPizzaServices pizzaservices,
                               ApplicationDbContext contextDb, 
                               IPizzaRepository pizzaRepository,
                               IWebHostEnvironment webHostEnvironment)
        {
            _pizzaServices = pizzaservices;
            _contextDb = contextDb;
            _pizzaRepository = pizzaRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pizzaServices.GetStandartPizzasAsync());
        }
        
        public IActionResult Create()
        {
            var ingredients = _pizzaServices.GetIngredients();
            ViewData["Ingredients"] = ingredients;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PizzaViewModelForAdmin pizzaViewModel)
        {
            var ingredients = _pizzaServices.GetIngredients();
            ViewData["Ingredients"] = ingredients;
            Pizza newPizza = new Pizza();
            
            var name = await _contextDb.Pizzas.FirstOrDefaultAsync(p => p.Name == pizzaViewModel.Name);
            if (name != null) 
            {
                ModelState.AddModelError("", $"Pizza {pizzaViewModel.Name} already exists");
                return View(pizzaViewModel);
            }

            newPizza.Name = pizzaViewModel.Name;
            newPizza.Price = pizzaViewModel.Price;

            if (ModelState.IsValid)
            {

                if (pizzaViewModel.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string imageName = Guid.NewGuid().ToString() + "_" + pizzaViewModel.ImageUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fileStream = new(filePath, FileMode.Create);
                    await pizzaViewModel.ImageUpload.CopyToAsync(fileStream);
                    fileStream.Close();

                    newPizza.ImagePath = imageName;
                }

                if (pizzaViewModel.Ingredients == null || pizzaViewModel.Ingredients.Count == 0)
                {
                    TempData["Error"] = "Не добален ни один ингредиент";
                    
                    return View(pizzaViewModel);
                }
                
                newPizza.Standart = true;

                newPizza.PizzaBase = _pizzaServices.GetPizzaBaseByName("стандартная");

                newPizza.Size = _pizzaServices.GetSizeByDiameter(32);

                newPizza.Ingredients = new List<Ingredient>();

                foreach (var item in pizzaViewModel.Ingredients)
                {
                    Ingredient ingredient = _pizzaServices.GetIngredientByName(item);
                    newPizza.Ingredients.Add(ingredient);
                }

                TempData["Success"] = await _pizzaServices.AddPizzaToDataBaseAsync(newPizza);

                return RedirectToAction("Index");
            }

            return View(pizzaViewModel);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            Pizza pizza = await _contextDb.Pizzas.FindAsync(id);

            return View(pizza);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, Pizza pizza)
         {
            if (ModelState.IsValid)
            {
                var name = await _contextDb.Pizzas.Where(p => p.Name == pizza.Name)
                                                  .Where(i => i.Id != pizza.Id)
                                                  .Select(p => pizza.Name)
                                                  .FirstOrDefaultAsync();
                if (name != null)
                {
                    ModelState.AddModelError("", $"Pizza {pizza.Name} already exists");
                    return View(pizza);
                }

                var existingPizza = await _contextDb.Pizzas.Where(p => p.Id == id)
                                               .Include(p => p.Ingredients)
                                               .Include(b => b.PizzaBase)
                                               .Include(s => s.Size)                                             
                                               .FirstOrDefaultAsync();

                if (pizza.ImageUpload != null)
                {
                    string uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string imageName = Guid.NewGuid().ToString() + "_" + pizza.ImageUpload.FileName;

                    string filePath = Path.Combine(uploadsDir, imageName);

                    FileStream fileStream = new(filePath, FileMode.Create);
                    await pizza.ImageUpload.CopyToAsync(fileStream);
                    fileStream.Close();

                    existingPizza.ImagePath = imageName;
                }

                existingPizza.Name = pizza.Name;
                existingPizza.Price = pizza.Price;

                _contextDb.Update(existingPizza);
                await _contextDb.SaveChangesAsync();

                TempData["Success"] = $"Pizza {existingPizza.Name} has been edited";
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            Pizza pizza = await _contextDb.Pizzas.FindAsync(id);

            _contextDb.Pizzas.Remove(pizza);
            await _contextDb.SaveChangesAsync();

            TempData["Success"] = $"Pizza {pizza.Name} has been deleted";

            return RedirectToAction("Index");
        }
    }
}
