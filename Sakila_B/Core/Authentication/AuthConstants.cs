using System.Security.Cryptography;
using System.Text;

namespace Sakila_B.Core.Authentication
{
    public static class AuthConstants
    {
        public const string ApiKeySectionName = "AuthenticationStrings:PrivateKey";
        public const string ApiKeyHeaderName = "Secret_Key";

        
    }
}
