using System.Collections.Generic;
using Exmo.Json.Converters;
using Newtonsoft.Json;

namespace Exmo.Models
{
    [JsonConverter(typeof(PairCollectionConverter))]
    public class PairCollection : List<Pair>
    {
        public PairCollection()
        {
        }

        public PairCollection(params Pair[] pairs)
            : base(pairs)
        {
        }

        public override string ToString()
        {
            return string.Join(",", this);
        }
    }
}
