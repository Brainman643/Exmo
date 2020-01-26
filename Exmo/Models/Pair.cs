using Exmo.JsonConverters;
using Newtonsoft.Json;

namespace Exmo.Models
{
    [JsonConverter(typeof(PairConverter))]
    public class Pair
    {
        public string BuyCurrency { get; set; }

        public string SellCurrency { get; set; }

        public Pair(string buyCurrency, string sellCurrency)
        {
            BuyCurrency = buyCurrency;
            SellCurrency = sellCurrency;
        }

        public override string ToString()
        {
            return BuyCurrency + "_" + SellCurrency;
        }

        public static Pair Parse(string value)
        {
            var parts = value.Split('_');
            return new Pair(parts[0], parts[1]);
        }

        public static explicit operator string(Pair value)
        {
            return value?.ToString();
        }

        public static implicit operator Pair(string value)
        {
            return value != null ? Parse(value) : null;
        }
    }
}
