using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PizzaWebAppAuthentication.Extentions
{
    public static class ConfigurationRegistrationExtensions
    {   
        public static IServiceCollection AddCustomConfiguration<TConfig>(this IServiceCollection services, string sectionName, ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TConfig : class
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            var serviceDescriptor = new ServiceDescriptor(
                typeof(TConfig),
                svc => {
                    var configuration = svc.GetRequiredService<IConfiguration>();
                    var section = configuration.GetSection(sectionName);

                    return section.Exists()
                        ? section.Get<TConfig>(cfg => cfg.BindNonPublicProperties = true)
                        : throw new ConfigurationErrorsException($"The section '{sectionName}' cannot be found in the configuration file");
                },
                lifetime);

            services.Add(serviceDescriptor);

            return services;
        }
    }
}
