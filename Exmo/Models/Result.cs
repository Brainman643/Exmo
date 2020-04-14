using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace Exmo.Models
{
    public class Result
    {
        [JsonProperty("result")]
        [SuppressMessage("Redundancy", "RCS1213:Remove unused member declaration.", Justification = "Necessary for serialization")]
        [SuppressMessage("Code Quality", "IDE0051:Remove unused private members", Justification = "Necessary for serialization")]
        private bool Succeeded { get; set; } = true;
    }
}
