using Microsoft.AspNetCore.Mvc.Filters;
using SakilaAPI.Core.Contants;
using SakilaAPI.Core.Exceptions;
using SakilaAPI.Core.Middlewares;

namespace SakilaAPI.Core.Authentication
{
    public class CustomAuthenticationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {

        }

        private void CheckAccessToken(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.Auth_AccessToken, out var accessToken))
            {
                throw new CustomException(StatusCodes.Status401Unauthorized, MessageSystem.msgAUTH_AUTHENTICATED_FAIL, MessageSystem.TOKEN_NOT_FOUND);
            }

            var claim = HelperIdentity.GetPrincipalFromExpiredToken(accessToken);
        }
    }
}
