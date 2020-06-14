using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Exmo.Helpers;
using Microsoft.Extensions.Options;

namespace Exmo
{
    public class AuthenticatedApiClient : ApiClient, IAuthenticatedApiClient
    {
        private readonly string _publicKey;
        private readonly string _secretKey;

        public AuthenticatedApiClient(IOptions<ExmoOptions> options, HttpClient httpClient)
            : base(options, httpClient)
        {
            _publicKey = options.Value.PublicKey;
            _secretKey = options.Value.SecretKey;
        }

        internal static string Sign(string key, string message)
        {
            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            var bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
            return HexConverter.ToHexString(bytes);
        }

        protected override async Task<FormUrlEncodedContent> GetContentAsync(List<KeyValuePair<string, string>> data)
        {
            data.Add(new KeyValuePair<string, string>("nonce", Nonce.Value.ToString(CultureInfo.InvariantCulture)));
            var content = await base.GetContentAsync(data);
            var message = await content.ReadAsStringAsync();
            var sign = Sign(_secretKey, message);
            content.Headers.Add("Sign", sign);
            content.Headers.Add("Key", _publicKey);
            return content;
        }
    }
}
