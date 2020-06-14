using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Exmo.Json
{
    public class RequirePropertiesContractResolver : DefaultContractResolver
    {
        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            var contract = base.CreateObjectContract(objectType);
            contract.ItemRequired ??= Required.AllowNull;
            return contract;
        }
    }
}
