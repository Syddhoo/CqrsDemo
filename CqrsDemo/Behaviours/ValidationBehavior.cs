using FluentValidation;
using MediatR;

namespace CqrsDemo.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(
            IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var results = await Task.WhenAll(
                _validators.Select(validator =>
                    validator.ValidateAsync(context, cancellationToken)));

            var failures = results
                .SelectMany(result => result.Errors)
                .Where(error => error is not null)
                .ToList();

            if (failures.Count > 0)
            {
                throw new ValidationException(failures);
            }

            return await next(cancellationToken);
        }
    }
}
