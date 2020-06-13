using System;
using Exmo.Json.Converters;
using Newtonsoft.Json;

namespace Exmo.Models
{
    [JsonConverter(typeof(CurrencyPairConverter))]
    public sealed class CurrencyPair : IEquatable<CurrencyPair>
    {
        public string BaseCurrency { get; }

        public string QuoteCurrency { get; }

        public CurrencyPair(string baseCurrency, string quoteCurrency)
        {
            BaseCurrency = baseCurrency ?? throw new ArgumentNullException(nameof(baseCurrency));
            QuoteCurrency = quoteCurrency ?? throw new ArgumentNullException(nameof(quoteCurrency));
        }

        public static CurrencyPair Parse(string value)
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
            return new CurrencyPair(parts[0], parts[1]);
        }

        public static explicit operator string(CurrencyPair value)
        {
            return value?.ToString();
        }

        public static implicit operator CurrencyPair(string value)
        {
            return value != null ? Parse(value) : null;
        }

        public static bool operator ==(CurrencyPair left, CurrencyPair right) => Equals(left, right);
        public static bool operator !=(CurrencyPair left, CurrencyPair right) => !Equals(left, right);

        public bool Equals(CurrencyPair other)
        {
            if (other is null)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            return BaseCurrency == other.BaseCurrency && QuoteCurrency == other.QuoteCurrency;
        }

        public override bool Equals(object obj)
        {
            return obj is CurrencyPair pair && Equals(pair);
        }

        public override int GetHashCode()
        {
            return ((BaseCurrency?.GetHashCode() ?? 0) * 397) ^ (QuoteCurrency?.GetHashCode() ?? 0);
        }

        public override string ToString()
        {
            return BaseCurrency + "_" + QuoteCurrency;
        }
    }
}
