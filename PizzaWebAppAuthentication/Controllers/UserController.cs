using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Services.RoleManagementService;

namespace PizzaWebAppAuthentication.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManagementService _userService;
        public UserController(UserManagementService userService)
        {
            _userService = userService;
        }

        //Details - отображает информацию о конкретном пользователе
        //Create - отображает форму для создания нового пользователя
        //Edit - отображает форму для редактирования информации о пользователе (это право должно быть у пользователя)
        //Delete - отображает форму для удаления пользователя (это право должно быть у пользователя)
        //Update - обновляет информацию о пользователе в базе данных
        //DeleteConfirmed - удаляет пользователя из базы данных
        //Manage - отображает форму для управления настройками профиля пользователя,
        //такими как изменение пароля или адреса электронной почты.
        //Tсли приложение позволяет пользователям отправлять друг другу сообщения,
        //то UserController может содержать дополнительные action для управления
        //сообщениями (например, SendMessage или ViewMessages).

        // GET: UserController
        public ActionResult Index() //отображает список всех пользователей для АДМИНа
        {
            return View(_userService.GetUsers());
        }

        // GET: UserController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
