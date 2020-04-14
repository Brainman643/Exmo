using System;
using System.Globalization;
using Newtonsoft.Json;

namespace Exmo.Json.Converters
{
    public class UnixTimeConverter : JsonConverter<DateTime>
    {
        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToUnixTime());
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            long seconds;
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    seconds = Convert.ToInt64(reader.Value, CultureInfo.InvariantCulture);
                    break;
                case JsonToken.String:
                    if (!long.TryParse((string)reader.Value, out seconds))
                    {
                        throw new ArgumentException($"{reader.Value} isn't a number.");
                    }
                    break;
                default:
                    throw new ArgumentException($"Unexpected token. Integer or String was expected, got {reader.TokenType}.");
            }

            return UnixTime.Create(seconds);
        }
    }
}
