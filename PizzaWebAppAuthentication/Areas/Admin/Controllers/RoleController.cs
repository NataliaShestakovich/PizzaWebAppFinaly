using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Services.RoleManagementService;

namespace PizzaWebAppAuthentication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "OnlyAdmin")]
    public class RoleController : Controller
    {
        private readonly UserManagementService _roleService;
        private readonly ILogger<RoleController> _logger;
        public RoleController(UserManagementService roleService,
                              ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                return View(_roleService.GetRoles() ?? new List<IdentityRole>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            }           
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
                        if (await _roleService.IsRoleExisteAsync(name))
                        {
                            ViewBag.Result = "Данная роль существует в списке ролей ";
                            return View();
                        }

                        return StatusCode(500);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    return StatusCode(500);
                }
            }
            ViewBag.Result = "Необходимо указать имя роли";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string name)
        {
            try
            {
                await _roleService.Delete(name);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(500);
            } 
        }
    }
}
