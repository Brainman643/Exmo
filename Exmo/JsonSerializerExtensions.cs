using System.IO;
using Newtonsoft.Json;

namespace Exmo
{
    public static class JsonSerializerExtensions
    {
        public static T Deserialize<T>(this JsonSerializer jsonSerializer, string value)
        {
            using(var reader = new StringReader(value))
            {
                return (T)jsonSerializer.Deserialize(reader, typeof(T));
            }
        }

        public static string Serialize(this JsonSerializer jsonSerializer, object value)
        {
            using (var writer = new StringWriter())
            {
                jsonSerializer.Serialize(writer, value);
                return writer.ToString();
            }
        }
    }
}
