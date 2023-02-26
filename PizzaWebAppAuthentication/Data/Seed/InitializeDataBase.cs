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
                var piz1 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "~/images/1.png" };
                var piz2 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "~/images/2.png" };
                var piz3 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "~/images/4.png" };
                var piz4 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "~/images/5.png" };
                var piz5 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "~/images/6.png" };
                var piz6 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "~/images/3.png" };
                var piz7 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.", ImageUrl = "~/images/1.png" };
                var piz8 = new Pizza { Name = "Capricciosa", Price = 70.00M, Description = "A normal pizza with a taste from the forest.",ImageUrl = "~/images/4.png" };

                var pizs = new List<Pizza>()
            {
                piz1, piz2, piz3, piz4, piz5, piz6, piz7, piz8
            };

                _context.Pizzas.AddRange(pizs);
                _context.SaveChanges();
            }
        }
    }
}
