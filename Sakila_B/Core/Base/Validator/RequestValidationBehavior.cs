using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Sakila_B.Core.Exceptions;

namespace Sakila_B.Core.Base.Validator
{
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.ToList().Any())
            {
                var validateResults = ValidateRequest(request);
                var failures = validateResults.SelectMany(result => result.Result.Errors).Where(f => f != null).ToList();
                if (failures.Count != 0)
                {
                    throw new StatusClientErrorException(failures);
                }
            }
            return next();
        }

        public IEnumerable<Task<ValidationResult>> ValidateRequest(TRequest request)
        {
            var context = new ValidationContext<TRequest>(request);
            return _validators.Select(async v => await v.ValidateAsync(context)).ToList();
        }
    }
}
