using System;
using Exmo.Models;
using Newtonsoft.Json;

namespace Exmo.JsonConverters
{
    public class PairCollectionConverter : JsonConverter<PairCollection>
    {
        public override void WriteJson(JsonWriter writer, PairCollection value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }

        public override PairCollection ReadJson(JsonReader reader, Type objectType, PairCollection existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override bool CanRead { get; } = false;
    }
}
