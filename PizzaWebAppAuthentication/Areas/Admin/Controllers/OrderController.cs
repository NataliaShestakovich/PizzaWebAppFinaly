using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Models.ViewModels.OrderViewModel;
using PizzaWebAppAuthentication.Options;
using PizzaWebAppAuthentication.Services.OrderServices;

namespace PizzaWebAppAuthentication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "OnlyAdmin")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderServices _orderServises;
        private readonly PizzaOption _pizzaOption;

        public OrderController(ILogger<OrderController> logger,
                                IOrderServices orderServises,
                                PizzaOption pizzaOption)
        {
            _logger = logger;
            _orderServises = orderServises;
            _pizzaOption = pizzaOption;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var dataOrders = await _orderServises.GetOrderViewModel() ?? new List<OrderViewModel>();

                return View(dataOrders);
            }
            catch (Exception ex)
            {
                var message = _pizzaOption.ErrorGetOrderData;

                _logger.LogError(ex, message);

                return StatusCode(500);
            }
        }
    }
}
