using Exmo.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Exmo.Json
{
    public static class JsonHelper
    {
        public static readonly JsonSerializer Serializer = JsonSerializer.Create(new JsonSerializerSettings
        {
            ContractResolver = new RequirePropertiesContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            Converters = { new UnixTimeConverter(true), new StringEnumConverter { NamingStrategy = new SnakeCaseNamingStrategy() } },
            FloatParseHandling = FloatParseHandling.Decimal
        });
    }
}
