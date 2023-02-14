using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PizzaWebAppAuthentication.Models.ViewModels.RoleManagementViewModels;
using PizzaWebAppAuthentication.Services.RoleManagementService;
using Serilog;
using System.Data;
using System.Linq;

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
                IdentityResult result = await _roleService.Delete(name);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersForRole()
        {
            var allRoles = _roleService.GetAllRoles();

            var modelForChoose = new UsersForRoleViewModel();

            modelForChoose.RolesSelectList = new List<SelectListItem>();

            foreach (var role in allRoles)
            {
                modelForChoose.RolesSelectList.
                    Add(new SelectListItem { Text = role.Name, Value = role.Name });                
            }
            
            return View(modelForChoose);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllUsersForRole(UsersForRoleViewModel modelForChoose)
        {
            var selectedRole = modelForChoose.SelectedRole;

            var usersForRoles = new List<string>(); // лист для хранения соответвующих юзеров

            if (selectedRole == null)
            {
                return NotFound();
            } 
            
            var selectedUsers = await _roleService.GetAllUsersForRole(selectedRole);
               
                foreach (var user in selectedUsers)
                {
                    usersForRoles.Add(user);
                }
                                    
            modelForChoose.UsersForSelectedRoles = usersForRoles;

            return View(modelForChoose);
        }


    }
}
