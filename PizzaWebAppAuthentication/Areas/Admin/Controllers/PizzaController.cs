using Microsoft.AspNetCore.Mvc;
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
        public PizzaController(ApplicationDbContext contextDb, IPizzaRepository pizzaRepository)
        {
            _contextDb = contextDb;
            _pizzaRepository = pizzaRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pizzaRepository.GetPizzas());
        }
    }
}
