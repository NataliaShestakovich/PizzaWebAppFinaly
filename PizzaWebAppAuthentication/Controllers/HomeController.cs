using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Models;
using PizzaWebAppAuthentication.Repositories.PizzaRepository;
using PizzaWebAppAuthentication.Services.PizzaServises;
using System.Diagnostics;

namespace PizzaWebAppAuthentication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPizzaServices _pizzaServices;

        public HomeController(ILogger<HomeController> logger, IPizzaServices pizzaServices)
        {
            _logger = logger;
            _pizzaServices = pizzaServices;
        }

        public async Task<IActionResult> Index()
        {
            var pizzas = await _pizzaServices.GetStandartPizzasAsync();

            return View(pizzas);
        }

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}