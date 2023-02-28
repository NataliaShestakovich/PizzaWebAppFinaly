using Microsoft.AspNetCore.Identity;
using PizzaWebAppAuthentication.Models.AppModels;
using System.Runtime.Intrinsics.X86;

namespace PizzaWebAppAuthentication.Data.Seed
{
    public class InitializeDataBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public InitializeDataBase(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager
        )
        {
            _context = context;
            _userManager = userManager;
            }

        public void InitializePizzasTableDb()
        {
            if (_context.Pizzas.Count() == 0)
            {
                var piz1 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "~/images/1.png" };
                var piz2 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "~/images/2.png" };
                var piz3 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "~/images/4.png" };
                var piz4 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "~/images/5.png" };
                var piz5 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "~/images/6.png" };
                var piz6 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "~/images/3.png" };
                var piz7 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "~/images/1.png" };
                var piz8 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.",ImageUrl = "~/images/4.png" };

                var pizs = new List<Pizza> {piz1, piz2, piz3, piz4, piz5, piz6, piz7, piz8};

                _context.Pizzas.AddRange(pizs);
                _context.SaveChanges();
            }
        }

        public void InitializeOrdersTableDb()
        {
            if (_context.OrderStatuses.Count() == 0)
            {
                var status1 = new OrderStatus { Id = 1, Name = "Accepted" };
                var status2 = new OrderStatus { Id = 2, Name = "Cooking" };
                var status3 = new OrderStatus { Id = 3, Name = "Completed" };
                var status4 = new OrderStatus { Id = 4, Name = "Interrupted" };

                var statuses = new List<OrderStatus> { status1, status2, status3, status4 };

                _context.OrderStatuses.AddRange(statuses);
                _context.SaveChanges();
            }
        }

        public async Task InitializeUsersTableDb ()
        {
            if (_context.Users.Count() == 0)
            {
                var user1 = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "bob@gmail.com",
                    UserName = "bob@gmail.com",
                    FirstName = "Bob",
                    LastName = "Smith",
                    PhoneNumber = "112223344",
                    EmailConfirmed = true
                };

                var user2 = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "john@gmail.com",
                    UserName = "john@gmail.com",
                    FirstName = "John",
                    LastName = "Dow",
                    PhoneNumber = "294445566",
                    EmailConfirmed = true
                };

                var user3 = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "sam@gmail.com",
                    UserName = "sam@gmail.com",
                    FirstName = "Sam",
                    LastName = "Johnson",
                    PhoneNumber = "447775566",
                    EmailConfirmed = true
                };

                var users = new List<ApplicationUser> { user1, user2, user3};

                foreach (var user in users)
                {
                   await AddUserToDb(user);
                }
            }

        }

        private async Task AddUserToDb(ApplicationUser user)
        {
            var existingUser = await _userManager.FindByEmailAsync(user!.Email);

            if (existingUser == null)
            {
                var _user = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (_user.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
            }
        }
    }
            
}
