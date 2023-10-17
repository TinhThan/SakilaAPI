namespace SakilaAPI.Core.Middlewares
{
    public class CurretnOption
    {
        public static AuthenticationString AuthenticationString { get; set; }= new AuthenticationString();
        public static Dictionary<string, string> Endpoints { get; set; }
    }

    public class AppSetting
    {
        public AuthenticationString AuthenticationStrings { get; set; }
        public Dictionary<string, string> Endpoints { get; set; }
    }

    public class AuthenticationString
    {
        public double ExpiredToken { get; set; }
        public string PrivateKey { get; set; }
        public string ApiName { get; set; }

        public AuthenticationString()
        {
        }
    }
}
