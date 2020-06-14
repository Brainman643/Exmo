using System;
using Exmo.Models;
using Newtonsoft.Json;

namespace Exmo.Json.Converters
{
    public class CurrencyPairConverter : JsonConverter<CurrencyPair>
    {
        public override void WriteJson(JsonWriter writer, CurrencyPair value, JsonSerializer serializer)
        {
            writer.WriteValue((string)value);
        }

        public override CurrencyPair ReadJson(JsonReader reader, Type objectType, CurrencyPair existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;
            return value;
        }
    }
}
