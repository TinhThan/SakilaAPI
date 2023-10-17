using SakilaAPI.Core.Contants;
using SakilaAPI.Core.Exceptions;

namespace SakilaAPI.Core.Middlewares
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
                var apiLogin = context.Request.Path.Value.Contains("/user/login");
                if (!apiLogin)
                {
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

        // Authentication Accesstoken
        private void CheckAccessToken_RefreshToken(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(AuthConstants.Auth_AccessToken, out var accessToken))
            {
                throw new CustomException(StatusCodes.Status401Unauthorized, MessageSystem.msgAUTH_AUTHENTICATED_FAIL, MessageSystem.TOKEN_NOT_FOUND);
            }

            if (!context.Request.Headers.TryGetValue(AuthConstants.Auth_RefreshToken, out var refreshToken))
            {
                throw new CustomException(StatusCodes.Status401Unauthorized, MessageSystem.msgAUTH_AUTHENTICATED_FAIL, MessageSystem.REFRESH_TOKEN_NOT_FOUND);
            }

        }
    }
}
