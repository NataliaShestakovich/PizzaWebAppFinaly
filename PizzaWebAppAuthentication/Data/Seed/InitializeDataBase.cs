using PizzaWebAppAuthentication.Models.AppModels;

namespace PizzaWebAppAuthentication.Data.Seed
{
    public class InitializeDataBase
    {
        private readonly ApplicationDbContext _context;

        public InitializeDataBase(ApplicationDbContext context)
        {
            _context= context;
        }

        public void InitializeDb()
        {
            if (_context.Pizzas.Count() == 0)
            {
                var piz1 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2a/Pizza_capricciosa.jpg" };
                var piz2 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2a/Pizza_capricciosa.jpg" };
                var piz3 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2a/Pizza_capricciosa.jpg" };
                var piz4 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2a/Pizza_capricciosa.jpg" };
                var piz5 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2a/Pizza_capricciosa.jpg" };
                var piz6 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2a/Pizza_capricciosa.jpg" };
                var piz7 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2a/Pizza_capricciosa.jpg" };
                var piz8 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2a/Pizza_capricciosa.jpg" };
                var piz9 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2a/Pizza_capricciosa.jpg" };
                var piz10 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/2/2a/Pizza_capricciosa.jpg" };

                var pizs = new List<Pizza>()
            {
                piz1, piz2, piz3, piz4, piz5, piz6, piz7, piz8, piz9, piz10
            };

                _context.Pizzas.AddRange(pizs);
                _context.SaveChanges();
            }
        }
    }
}
