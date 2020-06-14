using System.Collections.Generic;
using Exmo.Json.Converters;
using Newtonsoft.Json;

namespace Exmo.Models
{
    [JsonConverter(typeof(CurrencyPairCollectionConverter))]
    public class CurrencyPairCollection : List<CurrencyPair>
    {
        public CurrencyPairCollection()
        {
        }

        public CurrencyPairCollection(params CurrencyPair[] pairs)
            : base(pairs)
        {
        }

        public override string ToString()
        {
            return string.Join(",", this);
        }
    }
}
