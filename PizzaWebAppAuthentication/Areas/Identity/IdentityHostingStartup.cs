using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaWebAppAuthentication.Data;
using PizzaWebAppAuthentication.Models.AppModels;

[assembly: HostingStartup(typeof(PizzaWebAppAuthentication.Areas.Identity.IdentityHostingStartup))]
namespace PizzaWebAppAuthentication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
