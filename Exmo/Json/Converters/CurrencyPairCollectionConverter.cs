using System;
using Exmo.Models;
using Newtonsoft.Json;

namespace Exmo.Json.Converters
{
    public class CurrencyPairCollectionConverter : JsonConverter<CurrencyPairCollection>
    {
        public override void WriteJson(JsonWriter writer, CurrencyPairCollection value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override CurrencyPairCollection ReadJson(JsonReader reader, Type objectType, CurrencyPairCollection existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override bool CanRead { get; } = false;
    }
}
