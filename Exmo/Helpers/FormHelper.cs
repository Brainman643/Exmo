using System.Collections.Generic;
using System.Linq;
using Exmo.Json;
using Newtonsoft.Json.Linq;

namespace Exmo.Helpers
{
    public static class FormHelper
    {
        public static List<KeyValuePair<string, string>> GetValues(object value)
        {
            if (value is null)
            {
                return new List<KeyValuePair<string, string>>();
            }

            var jToken = JToken.FromObject(value, JsonHelper.Serializer);
            return GetValues(jToken);
        }

        public static List<KeyValuePair<string, string>> GetValues(JToken value)
        {
            return Flatten(value).ToList();
        }

        private static IEnumerable<KeyValuePair<string, string>> Flatten(JToken token, int deep = 0)
        {
            if (token == null)
            {
                yield break; // null values aren't serialized
            }

            switch (token.Type)
            {
                case JTokenType.Array:
                    var jArray = (JArray) token;
                    for (var i = 0; i < jArray.Count; i++)
                    {
                        foreach (var keyValuePair in Flatten(jArray[i], deep + 1))
                        {
                            var name = string.IsNullOrEmpty(keyValuePair.Key) ? "[]" : "[" + i + "]";
                            yield return new KeyValuePair<string, string>(name + keyValuePair.Key, keyValuePair.Value);
                        }
                    }

                    break;
                case JTokenType.Object:
                    var jObject = (JObject) token;
                    foreach (var property in jObject.Properties())
                    {
                        foreach (var keyValuePair in Flatten(property.Value, deep + 1))
                        {
                            var name = property.Name;
                            if (deep > 0)
                            {
                                name = "[" + name + "]";
                            }

                            yield return new KeyValuePair<string, string>(name + keyValuePair.Key, keyValuePair.Value);
                        }
                    }

                    break;
                default:
                    var value = (string) token;
                    yield return new KeyValuePair<string, string>(string.Empty, value);
                    break;
            }
        }
    }
}
