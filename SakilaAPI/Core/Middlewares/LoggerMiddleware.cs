namespace SakilaAPI.Core.Middlewares
{
    /// <summary>
    /// Middleware xử lý logger sau khi xử lý handler
    /// </summary>
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerMiddleware> _logger;

        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
    }
}
