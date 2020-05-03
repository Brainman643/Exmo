namespace Exmo
{
    public class ExmoOptions
    {
        public static string DefaultExmoApiUrl = "https://api.exmo.com/v1.1/";

        public string ApiPublicKey { get; set; }

        public string ApiSecretKey { get; set; }

        public string ExmoApiUrl { get; set; } = DefaultExmoApiUrl;
    }
}
