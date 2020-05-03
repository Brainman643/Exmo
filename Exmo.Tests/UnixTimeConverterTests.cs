using System;
using Exmo.Json;
using Exmo.Json.Converters;
using Newtonsoft.Json;
using Xunit;

namespace Exmo.Tests
{
    public class UnixTimeConverterTests
    {
        private class Fake
        {
            public DateTime DateTime { get; set; }
            public DateTime? NullableDateTime { get; set; }
            public DateTimeOffset DateTimeOffset { get; set; }
            public DateTimeOffset? NullableDateTimeOffset { get; set; }
        }

        private readonly JsonSerializer _serializer = JsonSerializer.Create(new JsonSerializerSettings
        {
            Converters = { new UnixTimeConverter(true) }
        });

        [Fact]
        public void SerializeDateTime()
        {
            var obj = new Fake
            {
                DateTime = DateTimeOffset.FromUnixTimeSeconds(1234).UtcDateTime,
                NullableDateTime = DateTimeOffset.FromUnixTimeSeconds(1234).UtcDateTime,
                DateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(1234),
                NullableDateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(1234),
            };
            var result = _serializer.Serialize(obj);

            Assert.Equal("{\"DateTime\":1234,\"NullableDateTime\":1234,\"DateTimeOffset\":1234,\"NullableDateTimeOffset\":1234}", result);
        }

        [Fact]
        public void DeserializeDateTime()
        {
            var result = _serializer.Deserialize<Fake>("{\"DateTime\":1234,\"NullableDateTime\":1234,\"DateTimeOffset\":1234,\"NullableDateTimeOffset\":1234}");

            Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1234).UtcDateTime, result.DateTime);
            Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1234).UtcDateTime, result.NullableDateTime);
            Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1234), result.DateTimeOffset);
            Assert.Equal(DateTimeOffset.FromUnixTimeSeconds(1234), result.NullableDateTimeOffset);
        }
    }
}
