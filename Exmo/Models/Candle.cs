using System;
using Exmo.Json.Converters;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class Candle
    {
        [JsonProperty("t")]
        [JsonConverter(typeof(UnixTimeConverter))]
        public DateTime Time { get; set; }

        [JsonProperty("o")]
        public decimal Open { get; set; }

        [JsonProperty("c")]
        public decimal Close { get; set; }

        [JsonProperty("h")]
        public decimal High { get; set; }

        [JsonProperty("l")]
        public decimal Low { get; set; }

        [JsonProperty("v")]
        public decimal Volume { get; set; }
    }
}
