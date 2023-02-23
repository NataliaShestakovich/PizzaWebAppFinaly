using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Models.ViewModels.RoleManagementViewModels;
using PizzaWebAppAuthentication.Models.ViewModels.UserManagementViewModels;
using PizzaWebAppAuthentication.Services.RoleManagementService;
using System.Data;

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
        public async Task<IActionResult> Index(UsersListViewModel? usersListViewModel) //отображает список всех пользователей для АДМИНа
        {
            List<ApplicationUser> userList = new();

            if (usersListViewModel != null)
            {
                if (!string.IsNullOrEmpty(usersListViewModel.SelectedRole))
                {
                    userList = await _userService.GetUsersByRoleAsync(usersListViewModel.SelectedRole);
                }
                else
                {
                    userList = _userService.GetUsers();
                }
                if (!string.IsNullOrEmpty(usersListViewModel.SelectedEmail))
                {
                    userList = userList.Where(e => e.Email == usersListViewModel.SelectedEmail).ToList();
                }
                if (!string.IsNullOrEmpty(usersListViewModel.SelectedFirstName))
                {
                    userList = userList.Where(e => e.FirstName == usersListViewModel.SelectedFirstName).ToList();
                }
            }
            else
            {
                userList = _userService.GetUsers();
            }
            
            var _usersListViewModel = new UsersListViewModel
            {
                 Users = userList.ToList(),
                 Role = _userService.GetSelectListRoles(),
                 FirstName = _userService.GetSelectListFirstName(),
                 Email = _userService.GetSelectListEmail(),
            };
              
            return View(_usersListViewModel);
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
