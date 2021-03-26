using System;

namespace ReviewApp.Common
{
    public static class ServiceProviderExtensions
    {
        public static TService GetService<TService>(this IServiceProvider provider)
        {
            return (TService) provider.GetService(typeof(TService));
        }
    }
}
