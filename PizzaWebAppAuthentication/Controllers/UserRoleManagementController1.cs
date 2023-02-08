using Microsoft.AspNetCore.Mvc;

namespace PizzaWebAppAuthentication.Controllers
{
    public class UserRoleManagementController1 : Controller
    {
        public IActionResult GetAllUsers()
        {
            return View();
        }

        public IActionResult GetUser(Guid id)
        {
            return View();
        }

        public IActionResult GetUser(string email)
        {
            return View();
        }

        public IActionResult EditUser(Guid id)
        {
            return View();
        }

        public IActionResult EditUser(string email)
        {
            return View();
        }

        public IActionResult DeleteUser(Guid id)
        {
            return View();
        }

        public IActionResult DeleteUser(string email)
        {
            return View();
        }



    }
}
