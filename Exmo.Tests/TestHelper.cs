using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Threading.Tasks;

namespace Exmo.Tests
{
    internal static class TestHelper
    {
        public static Func<HttpRequestMessage, Task> HandleQueryString(Action<NameValueCollection> action)
        {
            return request =>
            {
                var form = GetForm(Uri.UnescapeDataString(request.RequestUri.Query.Substring(1)));
                action(form);
                return Task.CompletedTask;
            };
        }

        public static Func<HttpRequestMessage, Task> HandleContent(Action<NameValueCollection> action)
        {
            return async request =>
            {
                var form = await TestHelper.GetFormAsync(request.Content);
                action(form);
            };
        }

        private static async Task<NameValueCollection> GetFormAsync(HttpContent content)
        {
            return GetForm(await content.ReadAsStringAsync());
        }

        private static NameValueCollection GetForm(string data)
        {
            var nameValueCollection = new NameValueCollection();
            foreach (var pair in data.Split('&', StringSplitOptions.RemoveEmptyEntries))
            {
                var parameter = pair.Split('=');
                nameValueCollection.Add(parameter[0], parameter[1]);
            }

            return nameValueCollection;
        }
    }
}
