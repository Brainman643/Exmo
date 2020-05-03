using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class UserInfo
    {
        [JsonProperty("uid")]
        public long Id { get; set; }

        public DateTimeOffset ServerDate {get;set;}

        public Dictionary<string, decimal> Balances { get; set; }

        public Dictionary<string, decimal> Reserved { get; set; }
    }
}
