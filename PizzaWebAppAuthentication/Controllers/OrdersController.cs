using Microsoft.AspNetCore.Mvc;

namespace PizzaWebAppAuthentication.Controllers
{
    //контроллер, который будет отвечать за отображение и управление заказами клиентов, а также
    //за обработку заказов и отправку уведомлений о новых заказах на почту пиццерии.
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
