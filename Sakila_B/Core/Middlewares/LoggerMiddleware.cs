using Sakila_B.Core.Contants;

namespace Sakila_B.Core.Middlewares
{
    /// <summary>
    /// Middleware xử lý logger sau khi xử lý handler
    /// </summary>
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerMiddleware> _logger;
        private readonly IConfiguration _configuration;

        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger, IConfiguration configuration)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var requestId = Guid.NewGuid();
            Stream originalBody = context.Response.Body;
            try
            {
                using (var responseBodyStream = new MemoryStream())
                {
                    context.Response.Body = responseBodyStream;

                    _logger.LogInformation($"Request {requestId}: {context.Request.Method} {context.Request.Path}");

                    await _next(context);

                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    string responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
                    int statusCode = context.Response.StatusCode;
                    if (statusCode >= 200 && statusCode < 300)
                    {
                        _logger.LogInformation($"Response {requestId} success: {responseBody}");
                    }
                    else if (statusCode >= 400 && statusCode < 500)
                    {
                        _logger.LogWarning($"Response {requestId} error client: {responseBody}");
                    }
                    else
                    {
                        _logger.LogError($"Response {requestId} error server: {responseBody}");
                    }

                    // Write the response body back to the original stream
                    responseBodyStream.Seek(0, SeekOrigin.Begin);
                    await responseBodyStream.CopyToAsync(originalBody);
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }

        // Authentication Secret Key
        public async Task CheckTokenExternalService(HttpContext context)
        {
            var urlApi = context.Request.Path.Value;
            if (!context.Request.Headers.TryGetValue(AuthConstants.Auth_Token, out var extractedApiKey))
            {
                await HelperIdentity.ThrowAuthException(context, MessageSystem.msgAUTH_AUTHENTICATED_FAIL, MessageSystem.TOKEN_NOT_FOUND);
                return;
            }

            if (!context.Request.Headers.TryGetValue(AuthConstants.Auth_Time, out var time) || !long.TryParse(time, out var dateTime))
            {
                await HelperIdentity.ThrowAuthException(context, MessageSystem.msgAUTH_AUTHENTICATED_FAIL, MessageSystem.TIME_NOT_FOUND);
                return;
            }

            if (!HelperIdentity.CheckTime(dateTime))
            {
                await HelperIdentity.ThrowAuthException(context, MessageSystem.msgAUTH_AUTHENTICATED_FAIL, MessageSystem.TOKEN_EXPIRED);
                return;
            }

            var privateKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
            if (!HelperIdentity.ComputeSHA256Hash(urlApi + dateTime.ToString() + privateKey).Equals(extractedApiKey))
            {
                await HelperIdentity.ThrowAuthException(context, MessageSystem.msgAUTH_AUTHENTICATED_FAIL, MessageSystem.TOKEN_INVALID);
                return;
            }
            await _next(context);
        }
    }
}
