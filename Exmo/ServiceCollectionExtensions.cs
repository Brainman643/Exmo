using Microsoft.Extensions.DependencyInjection;

namespace Exmo
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExmoApi(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IPublicApi, PublicApi>();
            serviceCollection.AddTransient<IAuthenticatedApi, AuthenticatedApi>();
            return serviceCollection;
        }
    }
}
