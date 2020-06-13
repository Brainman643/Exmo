using System.Text;

namespace Exmo.Helpers
{
    public static class HexConverter
    {
        public static string ToHexString(byte[] bytes)
        {
            var sb = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
            {
                sb.Append(b.ToString("x2")); // hex format
            }
            return sb.ToString();
        }
    }
}
