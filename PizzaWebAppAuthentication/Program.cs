using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Data.Seed;
using PizzaWebAppAuthentication.Extentions;
using PizzaWebAppAuthentication.Models.AppModels;
using PizzaWebAppAuthentication.Options;
using PizzaWebAppAuthentication.Repositories.IngredientRepository;
using PizzaWebAppAuthentication.Repositories.OrderRepository;
using PizzaWebAppAuthentication.Repositories.PizzaRepository;
using PizzaWebAppAuthentication.Services.IngredientServices;
using PizzaWebAppAuthentication.Services.OrderServices;
using PizzaWebAppAuthentication.Services.PizzaServises;
using PizzaWebAppAuthentication.Services.RoleManagementService;
using PizzaWebAppAuthentication.Services.Sendgrid;
using Serilog;
using Serilog.Events;

namespace PizzaWebAppAuthentication
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseSerilog((ctx, lc) => lc
                //.WriteTo.Console(LogEventLevel.Warning)
                .WriteTo.File(@"D:\Users\PizzaLog folder\logs\data.log", LogEventLevel.Warning));

                // Add services to the container.

                builder.Services.AddControllersWithViews();

                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                builder.Services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString));

                builder.Services.AddCustomConfiguration<PizzaOption>(PizzaOption.SectionName);

                builder.Services.AddDistributedMemoryCache();
                builder.Services.AddSession(options =>
                {
                    options.IdleTimeout = TimeSpan.FromMinutes(40);
                    options.Cookie.IsEssential = true;
                });

                builder.Services.AddDatabaseDeveloperPageExceptionFilter();

                builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                    options.User.RequireUniqueEmail = true;
                })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

                builder.Services.AddTransient<IEmailSender, EmailSender>();

                builder.Services.AddTransient<CreateDefaultUserService>();

                builder.Services.AddTransient<InitializeDataBase>();

                builder.Services.AddScoped<UserManagementService>(); 

                builder.Services.AddAuthorization(option =>
                        {
                            option.AddPolicy("OnlyAdmin", policyBuilder =>
                        policyBuilder.RequireRole("Admin"));
                        });

                builder.Services.AddTransient<IOrderRepository, OrderRepository>();
                builder.Services.AddTransient<IPizzaRepository, PizzaRepository>();
                builder.Services.AddTransient<IIngredientRepository, IngredientRepository>();

                builder.Services.AddTransient<IOrderServices, OrderServices>();
                builder.Services.AddTransient<IPizzaServices, PizzaServices>();
                builder.Services.AddTransient<IIngredientServises, IngredientServises>();


                var app = builder.Build();

                app.UseSession();

                await Startup.InitializeIdentities(app.Services);

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.UseMigrationsEndPoint();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseHttpsRedirection();

                app.UseStaticFiles();

                app.UseRouting();

                app.UseAuthentication();

                app.UseAuthorization();

                app.MapControllerRoute(
                    name: "Areas",
                    pattern: "{area:exists}/{controller=Pizza}/{action=Index}/{id?}");

                app.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                app.MapRazorPages();

                app.Run();

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}