using Microsoft.Extensions.DependencyInjection;
using ReviewApp.Authorization;
using ReviewApp.Configuration;

namespace ReviewApp.Common
{
    public static class DependencyInjectionRegistrationExtentions
    {
        public static IServiceCollection RegisterConfiguration(
             this IServiceCollection services)
        {
            services.AddTransient<IConfigurationContext, ConfigurationContext>();
            services.AddTransient<IAuthorizationService, FacebookWebhookAuthorizationService>();

            return services;
        }
    }
}
