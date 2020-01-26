using System;
using Exmo.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Exmo.JsonConverters
{
    public class OrderConverter : JsonConverter<Order>
    {
        public override void WriteJson(JsonWriter writer, Order value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override Order ReadJson(JsonReader reader, Type objectType, Order existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.StartArray)
            {
                throw new InvalidOperationException();
            }

            var array = (JArray)JToken.ReadFrom(reader);

            return new Order
            {
                Price = (decimal)array[0],
                Quantity = (decimal)array[1],
                Amount = (decimal)array[2]
            };
        }

        public override bool CanWrite { get; } = false;
    }
}
