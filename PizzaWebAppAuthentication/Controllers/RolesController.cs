using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Services.RoleManagementService;
using Serilog;

namespace PizzaWebAppAuthentication.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManagementService _roleService;
        public RolesController(RoleManagementService roleService)
        {
            _roleService= roleService;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View(_roleService.GetAllRoles());
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
                    IdentityResult result = await _roleService.CreateAsync(name);

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

    }
}
