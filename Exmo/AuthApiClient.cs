using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Exmo
{
    public class AuthApiClient : ApiClient, IAuthApiClient
    {
        private readonly string _publicKey;
        private readonly string _secretKey;

        public AuthApiClient(IOptions<ExmoOptions> options, HttpClient httpClient, ILogger<AuthApiClient> logger)
            : base(options, httpClient, logger)
        {
            _publicKey = options.Value.ApiPublicKey;
            _secretKey = options.Value.ApiSecretKey;
        }

        internal static string Sign(string key, string message)
        {
            using (var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                var bytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(message));
                return ConvertHelper.ToHexString(bytes);
            }
        }

        protected override async Task<FormUrlEncodedContent> GetContentAsync(List<KeyValuePair<string, string>> data)
        {
            data.Add(new KeyValuePair<string, string>("nonce", Convert.ToString(Nonce.Value)));
            var content = await base.GetContentAsync(data);
            var message = await content.ReadAsStringAsync();
            var sign = Sign(_secretKey, message);
            content.Headers.Add("Sign", sign);
            content.Headers.Add("Key", _publicKey);
            return content;
        }
    }
}
