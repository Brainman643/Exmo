namespace Exmo
{
    public class ExmoOptions
    {
        public static string DefaultExmoApiUrl = "https://api.exmo.com/v1.1/";

        public string PublicKey { get; set; }

        public string SecretKey { get; set; }

        public string ExmoApiUrl { get; set; } = DefaultExmoApiUrl;
    }
}
