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
            try
            {
                IdentityResult result = await _roleService.CreateAsync(name);

                if (result == IdentityResult.Success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new ArgumentException(nameof(name));                    
                }
            }
            catch (Exception e)
            {
                Log.Error($"Раз ошибка{e.Message}. {Environment.NewLine} {e.StackTrace}");
                return BadRequest();
            }            
        }

    }
}
