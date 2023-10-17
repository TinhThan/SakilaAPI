using System.Security.Cryptography;
using System.Text;

namespace Sakila_B.Core.Contants
{
    public static class AuthConstants
    {
        public const string ApiKeySectionName = "AuthenticationStrings:PrivateKey";
        public const string Auth_Token = "Token";
        public const string Auth_Time = "Time";
    }
}
