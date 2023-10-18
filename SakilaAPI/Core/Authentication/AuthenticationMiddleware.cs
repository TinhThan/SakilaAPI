using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SakilaAPI.Core.Contants;
using SakilaAPI.Core.Middlewares;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SakilaAPI.Core.Authentication
{
    public class AuthenticationMiddleware
    {
        private readonly ILogger<AuthenticationMiddleware> _logger;

        private readonly RequestDelegate _requestDelegate;

        public AuthenticationMiddleware(ILogger<AuthenticationMiddleware> logger, RequestDelegate requestDelegate)
        {
            _logger = logger;
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (IsAllowAnonymous(context))
                {
                    context.Items["StatusToken"] = "Anonymous";
                }
                else
                {
                    string accessToken = context.Request.Headers["AccessToken"].FirstOrDefault();
                    if (accessToken != null)
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(CurrentOption.AuthenticationString.PrivateKey);
                        tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key),
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ClockSkew = TimeSpan.Zero
                        }, out SecurityToken validatedToken);

                        var jwtToken = (JwtSecurityToken)validatedToken;
                        if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                        {
                            context.Items["StatusToken"] = MessageSystem.AUTH_AUTHENTICATED_VALIDATED;
                            context.Items["MessageToken"] = MessageSystem.msgAUTH_AUTHENTICATED_VERIFICATION;
                        }
                        var controller = context.Request.RouteValues["controller"].ToString();
                        var roles = JsonConvert.DeserializeObject<List<string>>(jwtToken.Claims.FirstOrDefault((x) => x.Type == "role")?.Value);
                        if (roles.Contains(controller.ToLower()))
                        {
                            context.Items["StatusToken"] = MessageSystem.AUTH_AUTHENTICATED_ACCEPT;
                            _logger.LogInformation($"request : {MessageSystem.AUTH_AUTHENTICATED_ACCEPT}");
                        }
                        else
                        {
                            context.Items["StatusToken"] = MessageSystem.AUTH_NOT_PERMISSION;
                            context.Items["MessageToken"] = MessageSystem.AUTH_NOT_PERMISSION;
                            _logger.LogWarning($"request : {MessageSystem.AUTH_NOT_PERMISSION}");
                        }
                    }
                    else
                    {
                        context.Items["StatusToken"] = MessageSystem.TOKEN_NOT_FOUND;
                        context.Items["MessageToken"] = MessageSystem.TOKEN_NOT_FOUND;
                        _logger.LogWarning($"request : {MessageSystem.TOKEN_NOT_FOUND}");
                    }
                }

                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                context.Items["StatusToken"] = HelperIdentity.StatusError(ex.Message);
                context.Items["MessageToken"] = HelperIdentity.MessageError(ex.Message);
            }
        }

        private bool IsAllowAnonymous(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var allowAnonymouAttribute = endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>();
            return allowAnonymouAttribute != null;
        }
    }
}
