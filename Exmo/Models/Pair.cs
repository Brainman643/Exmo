using System;
using Exmo.Json.Converters;
using Newtonsoft.Json;

namespace Exmo.Models
{
    [JsonConverter(typeof(PairConverter))]
    public sealed class Pair : IEquatable<Pair>
    {
        public string BuyCurrency { get; set; }

        public string SellCurrency { get; set; }

        public Pair(string buyCurrency, string sellCurrency)
        {
            BuyCurrency = buyCurrency ?? throw new ArgumentNullException(nameof(buyCurrency));
            SellCurrency = sellCurrency ?? throw new ArgumentNullException(nameof(sellCurrency));
        }

        public static Pair Parse(string value)
        {
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            var parts = value.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 2)
            {
                throw new FormatException($"Could not parse the pair \"{value}\".");
            }
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

        public static bool operator ==(Pair left, Pair right) => Equals(left, right);
        public static bool operator !=(Pair left, Pair right) => !Equals(left, right);

        public bool Equals(Pair other)
        {
            if (other is null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return BuyCurrency == other.BuyCurrency && SellCurrency == other.SellCurrency;
        }

        public override bool Equals(object obj)
        {
            return obj is Pair pair && Equals(pair);
        }

        public override int GetHashCode()
        {
            return ((BuyCurrency?.GetHashCode() ?? 0) * 397) ^ (SellCurrency?.GetHashCode() ?? 0);
        }

        public override string ToString()
        {
            return BuyCurrency + "_" + SellCurrency;
        }
    }
}
