using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaWebAppAuthentication.Models.ViewModels.RoleManagementViewModels;
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

        [HttpPost]
        public async Task<IActionResult> Delete (string name)
        {
            if (!string.IsNullOrEmpty(name)&&name!="Admin")
            {
                await _roleService.Delete(name);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersForRole()
        {
            var allRoles = _roleService.GetAllRoles();

            var modelForChoose = new UsersForRoleViewModel
            {
                RolesSelectList = new List<SelectListItem>()
            };

            if (allRoles != null)
            {
                foreach (var role in allRoles)
                {
                    modelForChoose.RolesSelectList.
                        Add(new SelectListItem { Text = role.Name, Value = role.Name });
                }
            }
            return View(modelForChoose);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllUsersForRole(UsersForRoleViewModel modelForChoose)
        {
            var selectedRole = modelForChoose.SelectedRole;

            if (selectedRole == null)
            {
                return NotFound();
            }

            modelForChoose.UsersForSelectedRoles = await _roleService.GetAllUsersForRole(selectedRole);

            if (modelForChoose.UsersForSelectedRoles == null 
                || modelForChoose.UsersForSelectedRoles.Count == 0 )
            {
                modelForChoose.UsersForSelectedRoles = new()
                {
                  $"Users with the role of the {selectedRole} weren't found"
                };
            }
            
            return View(modelForChoose);
        }
    }
}
