using Sakila_B.Core.Contants;
using Sakila_B.Core.Exceptions;

namespace Sakila_B.Core.Middlewares
{
    public class AuthorizeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthorizeMiddleware> _logger;
        private readonly IConfiguration _configuration;

        public AuthorizeMiddleware(RequestDelegate next, ILogger<AuthorizeMiddleware> logger, IConfiguration configuration)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var external_sub = context.Request.Headers["role"].FirstOrDefault();
                if (!string.IsNullOrEmpty(external_sub) && external_sub == "external_sub")
                {
                    CheckTokenExternalService(context);
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Response authorize: {ex.Message}");
                if (ex is CustomException e)
                {
                    await HelperIdentity.ThrowAuthException(context, e?.Title ?? MessageSystem.msgAUTH_AUTHENTICATED_FAIL,
                                    e?.Description ?? MessageSystem.AUTH_AUTHENTICATED_ERROR);
                    return;
                }
                await HelperIdentity.ThrowAuthException(context, MessageSystem.msgAUTH_AUTHENTICATED_FAIL, ex.Message);
            }
        }

        // Authentication Secret Key
        private void CheckTokenExternalService(HttpContext context)
        {
            var urlApi  = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}{context.Request.Path}{context.Request.QueryString}";
            if (!context.Request.Headers.TryGetValue(AuthConstants.Auth_Token, out var extractedApiKey))
            {
                throw new CustomException(StatusCodes.Status401Unauthorized, MessageSystem.msgAUTH_AUTHENTICATED_FAIL, MessageSystem.TOKEN_NOT_FOUND);
            }

            if (!context.Request.Headers.TryGetValue(AuthConstants.Auth_Time, out var time) || !long.TryParse(time, out var dateTime))
            {
                throw new CustomException(StatusCodes.Status401Unauthorized, MessageSystem.msgAUTH_AUTHENTICATED_FAIL, MessageSystem.TIME_NOT_FOUND);
            }

            if (!HelperIdentity.CheckTime(dateTime))
            {
                throw new CustomException(StatusCodes.Status401Unauthorized, MessageSystem.msgAUTH_AUTHENTICATED_FAIL, MessageSystem.TOKEN_EXPIRED);
            }

            var privateKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
            if (!HelperIdentity.ComputeSHA256Hash(urlApi + dateTime.ToString() + privateKey).Equals(extractedApiKey))
            {
                throw new CustomException(StatusCodes.Status401Unauthorized, MessageSystem.msgAUTH_AUTHENTICATED_FAIL, MessageSystem.TOKEN_INVALID);
            }
        }
    }
}
