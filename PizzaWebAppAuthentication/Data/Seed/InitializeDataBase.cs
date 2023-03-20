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

        public async Task InitializeDb()
        {
            InitializeIngredientsTableDb();
            InitializeSizeTableDb();
            InitializeBaseTableDb();
            InitializePizzasTableDb();
            await InitializeUsersTableDb();
        }


        private void InitializePizzasTableDb()
        {
            if (_context.Pizzas.Count() == 0)
            {
                var piz1 = new Pizza { Name = "Капричиоза", Standart = true, Price = 70.00M, ImagePath = "1.png" };
                var piz2 = new Pizza { Name = "Сытная", Standart = true, Price = 75.00M, ImagePath = "2.png" };
                var piz3 = new Pizza { Name = "Гавайская", Standart = true, Price = 60.00M, ImagePath = "4.png" };
                var piz4 = new Pizza { Name = "Везувий", Standart = true, Price = 70.00M, ImagePath = "5.png" };
                var piz5 = new Pizza { Name = "Студенческая", Standart = true, Price = 70.00M, ImagePath = "6.png" };
                var piz6 = new Pizza { Name = "Охотничья", Standart = true, Price = 60.00M, ImagePath = "3.png" };
                var piz7 = new Pizza { Name = "Сырная", Standart = true, Price = 75.00M, ImagePath = "1.png" };
                var piz8 = new Pizza { Name = "Пепперони", Standart = true, Price = 75.00M, ImagePath = "4.png" };
                var piz9 = new Pizza { Name = "Клиентская", Standart = false, Price = 75.00M, ImagePath = "1.png" };
                var piz10 = new Pizza { Name = "Клиентская", Standart = false, Price = 75.00M, ImagePath = "4.png" };


                var pizs = new List<Pizza> { piz1, piz2, piz3, piz4, piz5, piz6, piz7, piz8, piz9, piz10 };

                var pizzaBase = _context.Bases.FirstOrDefault();
                if (pizzaBase != null)
                {
                    foreach (var pizza in pizs)
                    {
                        pizza.PizzaBase = pizzaBase;
                    }
                }

                var size = _context.Sizes.FirstOrDefault();
                if (size != null)
                {
                    foreach (var pizza in pizs)
                    {
                        pizza.Size = size;
                    }
                }

                var ingredients = _context.Ingredients.ToList();

                foreach (var ingredient in ingredients)
                {
                    foreach (var pizza in pizs)
                    {
                        pizza.Ingredients.Add(ingredient);
                    }
                }

                _context.Pizzas.AddRange(pizs);
                _context.SaveChanges();
            }
        }

        private void InitializeIngredientsTableDb()
        {
            if (_context.Ingredients.Count() == 0)
            {
                var ingr3 = new Ingredient { Name = "Кетчуп", Price = 2, PortionGrams = 15 };
                var ingr4 = new Ingredient { Name = "Грибы", Price = 3, PortionGrams = 20 };
                var ingr5 = new Ingredient { Name = "Сыр", Price = 5, PortionGrams = 25 };
                var ingr6 = new Ingredient { Name = "Ветчина", Price = 12, PortionGrams = 30 };
                var ingr7 = new Ingredient { Name = "Курица", Price = 9, PortionGrams = 35 };
                var ingr8 = new Ingredient { Name = "Помидоры", Price = 3, PortionGrams = 35 };

                var ingrs = new List<Ingredient> { ingr3, ingr4, ingr5, ingr6, ingr7, ingr8 };

                _context.Ingredients.AddRange(ingrs);
                _context.SaveChanges();
            }
        }
        private void InitializeSizeTableDb()
        {
            if (_context.Sizes.Count() == 0)
            {
                var size1 = new Size { Name = "маленькая", Diameter = 28 };
                var size2 = new Size { Name = "средняя", Diameter = 32 };
                var size3 = new Size { Name = "большая", Diameter = 45 };

                var sizes = new List<Size> { size1, size2, size3 };

                _context.Sizes.AddRange(sizes);
                _context.SaveChanges();
            }
        }

        private void InitializeBaseTableDb()
        {
            if (_context.Bases.Count() == 0)
            {
                var base1 = new PizzaBase { Name = "тонкая", Price = 1.2M };
                var base2 = new PizzaBase { Name = "стандартная", Price = 1.5M };
                var base3 = new PizzaBase { Name = "толстая", Price = 1.7M };

                var bases = new List<PizzaBase> { base1, base2, base3 };

                _context.Bases.AddRange(bases);
                _context.SaveChanges();
            }
        }

        private async Task InitializeUsersTableDb()
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

                var users = new List<ApplicationUser> { user1, user2, user3 };

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
