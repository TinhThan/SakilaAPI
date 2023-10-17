using System.Security.Cryptography;
using System.Text;

namespace SakilaAPI.Core.Contants
{
    public static class AuthConstants
    {
        public const string ApiKeySectionName = "AuthenticationStrings:PrivateKey";
        public const string Auth_AccessToken = "AccessToken";
        public const string Auth_RefreshToken = "RefreshToken";
    }
}
