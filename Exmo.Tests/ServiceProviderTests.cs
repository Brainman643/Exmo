using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Exmo.Tests
{
    public class ServiceProviderTests
    {
        [Fact]
        public void AddExmoApi()
        {
            var services = new ServiceCollection();
            services.AddExmoApi();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                var publicApi = serviceProvider.GetRequiredService<IPublicApi>();

                Assert.NotNull(publicApi);
            }
        }
    }
}
