using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.ViewModels.CartViewModeles;
using PizzaWebAppAuthentication.Repositories;

namespace PizzaWebAppAuthentication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PizzaController: Controller
    {
        private readonly ApplicationDbContext _contextDb;
        private readonly IPizzaRepository _pizzaRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PizzaController(ApplicationDbContext contextDb, 
                              IPizzaRepository pizzaRepository,
                              IWebHostEnvironment webHostEnvironment)
        {
            _contextDb = contextDb;
            _pizzaRepository = pizzaRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Create()
        {
            
            return View();
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pizzaRepository.GetPizzas());
        }
    }
}
