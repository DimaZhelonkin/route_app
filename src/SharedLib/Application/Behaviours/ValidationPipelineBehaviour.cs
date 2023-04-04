using Ark.SharedLib.Common.Results;
using Ark.SharedLib.Common.Results.Extensions;
using FluentValidation;
using MediatR;

namespace Ark.SharedLib.Application.Behaviours;

public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    #region IPipelineBehavior<TRequest,TResponse> Members

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationResults =
            await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
        var errors = validationResults
                     .SelectMany(r => r.Errors)
                     .Where(f => f is not null)
                     .Select(x => x.AsError())
                     .Distinct()
                     .ToArray();
        if (errors.Any()) return CreateValidationResult<TResponse>(errors);

        return await next();
    }

    #endregion

    private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
            return (ValidationResult.WithErrors(errors) as TResult)!;

        var validationResult = typeof(ValidationResult<>)
                               .GetGenericTypeDefinition()
                               .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
                               .GetMethod(nameof(ValidationResult.WithErrors))!
                               .Invoke(null, new object?[] {errors})!;

        return (TResult)validationResult;
    }
}