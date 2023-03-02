using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Services.RoleManagementService;
using Serilog;

namespace PizzaWebAppAuthentication.Controllers
{
    [Authorize(Policy = "OnlyAdmin")]
    public class RoleController : Controller
    {
        private readonly UserManagementService _roleService;
        public RoleController(UserManagementService roleService)
        {
            _roleService= roleService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View(_roleService.GetRoles());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                try
                {
                    IdentityResult result = await _roleService.CreateRoleAsync(name);

                    if (result == IdentityResult.Success)
                    {
                       return RedirectToAction("Index");
                    }
                    else
                    {
                        if (await _roleService.IsRoleExiste(name))
                        {
                            ViewBag.Result = "Данная роль существует в списке ролей ";
                            return View();
                        }

                        return StatusCode(500);
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"Ошибка{e.Message}. {Environment.NewLine} {e.StackTrace}");
                    return StatusCode(500);
                }
            }
            ViewBag.Result = "Необходимо указать имя роли";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete (string name)
        {
            if (!string.IsNullOrEmpty(name) && name!="Admin" && name != "User")
            {
                var isExistUser = await _roleService.GetUsersByRoleAsync(name);
                if (isExistUser.Count() <= 0)
                {
                    await _roleService.Delete(name);
                }                
            }

            return RedirectToAction("Index");
        }        
    }
}
