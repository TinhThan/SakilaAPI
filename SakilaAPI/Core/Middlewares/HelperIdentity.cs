using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SakilaAPI.Core.Middlewares
{
    public static class HelperIdentity
    {
        private const string _Salt = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        private const int _iterCount = 10000;

        public static async Task ThrowAuthException(HttpContext context, string message, string detail)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = message,
                Detail = detail
            });
        }

        public static string ComputeSHA256Hash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string HashPasswordSalt(string password)
        {
            // derive a 256-bit subkey (use HMACSHA512 with 10,000 iterations)
            var hashed = KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.UTF8.GetBytes(_Salt),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: _iterCount,
                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(hashed);
        }

        public static string GenerateToken(Dictionary<string, string> liststrClaim)
        {
            List<Claim> listClaim = new List<Claim>();
            liststrClaim.ToList().ForEach(x =>
            {
                listClaim.Add(new Claim(x.Key, x.Value));
            });

            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(CurrentOption.AuthenticationString.PrivateKey));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = CurrentOption.AuthenticationString.Issuer,
                Subject = new ClaimsIdentity(listClaim),
                Expires = DateTime.Now.AddMinutes(CurrentOption.AuthenticationString.ExpiredToken),
                SigningCredentials = signinCredentials
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
