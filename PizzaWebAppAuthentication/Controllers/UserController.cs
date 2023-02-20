using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PizzaWebAppAuthentication.Controllers
{
    public class UserController : Controller
    {
        //TODO Index - отображает список всех пользователей
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
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
