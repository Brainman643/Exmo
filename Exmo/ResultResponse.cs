using Newtonsoft.Json;

namespace Exmo
{
    internal class ResultResponse
    {
        [JsonProperty("result", Required = Required.Default)]
        public bool Succeeded { get; set; } = true;

        [JsonProperty(Required = Required.Default)]
        public string Error { get; set; }
    }
}
