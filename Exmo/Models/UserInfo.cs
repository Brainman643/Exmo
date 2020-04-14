using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class UserInfo
    {
        [JsonProperty("uid")]
        public long Id { get; set; }

        [JsonProperty("server_date")]
        public DateTime ServerDate {get;set;}

        [JsonProperty("balances")]
        public Dictionary<string, decimal> Balances { get; set; }

        [JsonProperty("reserved")]
        public Dictionary<string, decimal> Reserved { get; set; }
    }
}
