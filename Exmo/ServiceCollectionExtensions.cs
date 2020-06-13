using System;
using Microsoft.Extensions.DependencyInjection;

namespace Exmo
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExmoApi(this IServiceCollection services, Action<ExmoOptions> configureOptions = null)
        {
            services.AddHttpClient<IApiClient, ApiClient>(ExmoDefaults.HttpClientName);
            services.AddHttpClient<IAuthenticatedApiClient, AuthenticatedApiClient>(ExmoDefaults.HttpClientName);
            services.AddTransient<IPublicApi, PublicApi>();
            services.AddTransient<IAuthenticatedApi, AuthenticatedApi>();

            services.Configure(configureOptions ?? (_ => { }));

            return services;
        }
    }
}
