using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Exmo.Json.Converters
{
    public class UnixTimeConverter : DateTimeConverterBase
    {
        public UnixTimeConverter()
            :this(false)
        {
        }

        public UnixTimeConverter(bool useSeconds)
        {
            UseSeconds = useSeconds;
        }

        public bool UseSeconds { get; set; }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DateTimeOffset dateTimeOffset))
            {
                dateTimeOffset = (DateTime)value;
            }
            var unixTime = UseSeconds ? dateTimeOffset.ToUnixTimeSeconds() : dateTimeOffset.ToUnixTimeMilliseconds();
            writer.WriteValue(unixTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            long unixTime;
            switch (reader.TokenType)
            {
                case JsonToken.Integer:
                    unixTime = (long)reader.Value!;
                    break;
                case JsonToken.String:
                    if (!long.TryParse((string)reader.Value!, out unixTime))
                    {
                        throw new ArgumentException($"{reader.Value} isn't a number.");
                    }
                    break;
                default:
                    throw new ArgumentException($"Unexpected token. Integer or String was expected, got {reader.TokenType}.");
            }

            var dateTimeOffset = UseSeconds
                ? DateTimeOffset.FromUnixTimeSeconds(unixTime)
                : DateTimeOffset.FromUnixTimeMilliseconds(unixTime);

            if (IsDateTimeOffset(objectType))
            {
                return dateTimeOffset;
            }
            return dateTimeOffset.UtcDateTime;
        }

        private static bool IsDateTimeOffset(Type type)
            => type == typeof(DateTimeOffset) || type == typeof(DateTimeOffset?);
    }
}
