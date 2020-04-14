using System;
using Microsoft.Extensions.DependencyInjection;

namespace Exmo
{
    public static class ServiceCollectionExtensions
    {
        private const string HttpClientName = "ExmoApiClient";

        public static IServiceCollection AddExmoApi(this IServiceCollection services, Action<ExmoOptions> configureOptions = null)
        {
            services.AddLogging();

            services.AddHttpClient<IApiClient, ApiClient>(HttpClientName);
            services.AddHttpClient<IAuthApiClient, AuthApiClient>(HttpClientName);
            services.AddTransient<IPublicApi, PublicApi>();
            services.AddTransient<IAuthenticatedApi, AuthenticatedApi>();

            services.Configure(configureOptions ?? (_ => { }));

            return services;
        }
    }
}
