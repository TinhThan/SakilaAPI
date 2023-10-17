using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Sakila_B.Core.Middlewares
{
    public static class HelperIdentity
    {
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

        public static bool CheckTime(long dateTime)
        {
            DateTimeOffset dateTimeOffset = DateTimeOffset.UtcNow;
            long timeConvert = dateTimeOffset.ToUnixTimeSeconds();

            if (timeConvert - dateTime > 300000)
            {
                return false;
            }
            return true;
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
    }
}
