using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Sakila_B.Core.Authentication
{
    public class ApiKeyAuthFilter : IAuthorizationFilter
    {
        private readonly IConfiguration _configuration;
        private string _key;

        public ApiKeyAuthFilter(IConfiguration configuration)
        {
            _configuration = configuration;

        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var checkTime = CheckTime(context);
            if (!checkTime)
            {
                context.Result = new UnauthorizedObjectResult("Over time, Please call back");
                return;
            }
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Api Key missing");
                return;
            }

            var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new UnauthorizedObjectResult("Invalid Api Key");
                return; 
            }
        }

        public bool CheckTime(AuthorizationFilterContext context)
        {
            long dateTime = long.Parse(context.HttpContext.Request.Headers["Time"]);

            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            DateTime localTime = DateTime.Now;
            DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTimeToUtc(localTime, localTimeZone);
            long timeConvert = dateTimeOffset.ToUnixTimeSeconds();

            if (timeConvert - dateTime > 300000)
            {
                return false;
            }
            return true;
        }
    }
}
