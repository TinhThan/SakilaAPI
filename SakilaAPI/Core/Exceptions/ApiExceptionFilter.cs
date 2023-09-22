using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SakilaAPI.Core.Contants;
using System.Net.WebSockets;

namespace SakilaAPI.Core.Exceptions
{
    /// <summary>
    /// Attribute xử xý exception cho controller action
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHanlders;

        /// <summary>
        /// Ánh xạ các exception cho từ handle
        /// </summary>
        public ApiExceptionFilterAttribute()
        {
            _exceptionHanlders = new Dictionary<Type, Action<ExceptionContext>>
            {
                {typeof(StatusClientErrorException),HandleClientErrorException },
                {typeof(StatusSuccessException),HandleSuccessException },
                {typeof(StatusServerErrorException),HandleServerException }
            };
        }

        /// <summary>
        /// Xử lý các exception bình thường
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptionHanlders.ContainsKey(type))
            {
                _exceptionHanlders[type].Invoke(context);
                return;
            }
            HandleUnknowException(context);
            base.OnException(context);
        }

        /// <summary> 
        /// Handler xử lý exception unknow
        /// </summary>
        /// <param name="context"></param>
        private static void HandleUnknowException(ExceptionContext context)
        {
            var details = new ProblemDetails()
            {
                Title = MessageSystem.SERVER_ERROR,
                Status = StatusCodes.Status500InternalServerError,
                Detail = context.Exception.InnerException == null ? context.Exception.Message : context.Exception.InnerException.Message
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handler xử lý exception of server - code 5xx
        /// </summary>
        /// <param name="context"></param>
        private static void HandleServerException(ExceptionContext context)
        {
            var exception = (StatusServerErrorException)context.Exception ?? new StatusServerErrorException();

            context.Result = new JsonResult(new ExceptionResponse()
            {
                Status = exception.Code,
                Title = exception.Title,
                Description = exception.Description == null ? exception.Descriptions : exception.Description
            })
            {
                StatusCode = exception.Code
            };

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handler xử lý exception of client(code 4xx)
        /// </summary>
        /// <param name="context"></param>
        private static void HandleClientErrorException(ExceptionContext context)
        {
            var exception = (StatusClientErrorException)context.Exception ?? new StatusClientErrorException();

            var details = new ValidationProblemDetails(exception.Errors)
            {
                Title = exception.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        /// <summary>
        /// Handler xử lý success exception - code 2xx
        /// </summary>
        /// <param name="context"></param>
        private static void HandleSuccessException(ExceptionContext context)
        {
            var exception = (StatusSuccessException)context.Exception ?? new StatusSuccessException();

            context.Result = new JsonResult(new ExceptionResponse()
            {
                Status = exception.Code,
                Title = exception.Title,
                Description = exception.Description == null ? exception.Descriptions : exception.Description
            })
            {
                StatusCode = exception.Code
            };

            context.ExceptionHandled = true;
        }
    }
}
