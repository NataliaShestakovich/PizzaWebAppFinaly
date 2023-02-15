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
        public IActionResult GetUsersByRole()
        {
            var roles = _roleService.GetRoles();

            var roleUsersViewModel = new RoleUsersViewModel
            {
                SelectListRole = new List<SelectListItem>(),
                Users = new List<string>()
            };

            if (roles != null)
            {
                foreach (var role in roles)
                {
                    roleUsersViewModel.SelectListRole.
                        Add(new SelectListItem { Text = role.Name, Value = role.Name });
                }
            }
            return View(roleUsersViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GetUsersByRole(RoleUsersViewModel roleUsersViewModel)
        {
            var selectedRole = roleUsersViewModel.SelectedRole;

            if (selectedRole == null)
            {
                return NotFound();
            }

            roleUsersViewModel.Users = await _roleService.GetUsersByRole(selectedRole);

            var allRoles = _roleService.GetRoles();

            roleUsersViewModel.SelectListRole = new List<SelectListItem>();

            foreach (var role in allRoles)
            {
                var selectListItem = new SelectListItem { Text = role.Name, Value = role.Name };

                roleUsersViewModel.SelectListRole.Add(selectListItem);
            }

            return View(roleUsersViewModel);
        }


    }
}
