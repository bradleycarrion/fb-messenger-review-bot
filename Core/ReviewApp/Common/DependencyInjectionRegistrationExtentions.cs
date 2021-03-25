using Microsoft.Extensions.DependencyInjection;
using ReviewApp.Configuration;

namespace ReviewApp.Common
{
    public static class DependencyInjectionRegistrationExtentions
    {
        public static IServiceCollection RegisterConfiguration(
             this IServiceCollection services)
        {
            services.AddTransient<IConfigurationContext, ConfigurationContext>();

            return services;
        }
    }
}
