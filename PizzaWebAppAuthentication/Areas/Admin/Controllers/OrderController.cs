using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Services.OrderServices;

namespace PizzaWebAppAuthentication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "OnlyAdmin")]
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderServices _orderServises;
        
        public OrderController(ILogger<OrderController> logger,
                                IOrderServices orderServises)
        {
            _logger = logger;
            _orderServises = orderServises;            
        }

        public async Task<IActionResult> Index()
        {
            var dataOrders = await _orderServises.GetOrderViewModel();

            return View(dataOrders);
        }
    }
}
