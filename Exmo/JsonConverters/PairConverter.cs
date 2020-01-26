using System;
using Exmo.Models;
using Newtonsoft.Json;

namespace Exmo.JsonConverters
{
    public class PairConverter : JsonConverter<Pair>
    {
        public override void WriteJson(JsonWriter writer, Pair value, JsonSerializer serializer)
        {
            writer.WriteValue((string)value);
        }

        public override Pair ReadJson(JsonReader reader, Type objectType, Pair existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = (string)reader.Value;
            return value;
        }
    }
}
