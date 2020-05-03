using System;
using Microsoft.Extensions.DependencyInjection;

namespace Exmo
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExmoApi(this IServiceCollection services, Action<ExmoOptions> configureOptions = null)
        {
            services.AddLogging();

            services.AddHttpClient<IApiClient, ApiClient>(HttpClientDefaults.ExmoHttpClientName);
            services.AddHttpClient<IAuthApiClient, AuthApiClient>(HttpClientDefaults.ExmoHttpClientName);
            services.AddTransient<IPublicApi, PublicApi>();
            services.AddTransient<IAuthenticatedApi, AuthenticatedApi>();

            services.Configure(configureOptions ?? (_ => { }));

            return services;
        }
    }
}
