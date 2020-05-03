using System;
using Newtonsoft.Json;

namespace Exmo.Json.Converters
{
    public class UnixTimeConverter : JsonConverter<DateTime>
    {
        private readonly bool _useSeconds;

        public UnixTimeConverter()
            :this(false)
        {
        }

        public UnixTimeConverter(bool useSeconds)
        {
            _useSeconds = useSeconds;
        }

        public override void WriteJson(JsonWriter writer, DateTime value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToUnixTime());
        }

        public override DateTime ReadJson(JsonReader reader, Type objectType, DateTime existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            long value;
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    value = (long)reader.Value!;
                    break;
                case JsonToken.String:
                    if (!long.TryParse((string)reader.Value!, out value))
                    {
                        throw new ArgumentException($"{reader.Value} isn't a number.");
                    }
                    break;
                default:
                    throw new ArgumentException($"Unexpected token. Integer or String was expected, got {reader.TokenType}.");
            }

            return _useSeconds
                ? UnixTime.FromSeconds(value)
                : UnixTime.FromMilliseconds(value);
        }
    }
}
