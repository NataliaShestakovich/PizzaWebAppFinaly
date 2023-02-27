using Microsoft.AspNetCore.Mvc;
using PizzaWebAppAuthentication.Models.AppModels;
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

        public async Task<IActionResult> Details(Guid id)
        {
            var user = await _userService.GetUserByIDAsync(id);

            var userData = new UserDataViewModel
            {
                Id = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user?.PhoneNumber??string.Empty,
                Role = await _userService.GetRoleByUserAsync(user),
                DateLastOrder = DateTime.Today.AddDays(new Random().Next(-5,-1))
            };
            return View(userData);
        }

        // GET: UserController/Create
        public IActionResult Create()
        {
            HttpContext httpContext = HttpContext;

            string host = httpContext.Request.Host.Host;
            
            int port = httpContext.Request.Host.Port.GetValueOrDefault();

            return Redirect($"https://{host}:{port}/Identity/Account/Register");
        }        
    }
}
