using Exmo.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Exmo
{
    public class JsonHelper
    {
        public static readonly JsonSerializer Serializer = JsonSerializer.Create(new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            },
            Converters = { new UnixTimeConverter() }
        });
    }
}
