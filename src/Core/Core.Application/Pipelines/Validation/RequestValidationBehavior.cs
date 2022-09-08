using FluentValidation;
using MediatR;

namespace Core.Application.Pipelines.Validation;

public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        var validationContext = new ValidationContext<object>(request);

        var validationFailures = _validators.Select(validator => validator.Validate(validationContext)).SelectMany(validationResult => validationResult.Errors).Where(validationFailure => validationFailure != null).ToList();

        if (validationFailures.Any()) throw new ValidationException(validationFailures);

        return next();
    }
}