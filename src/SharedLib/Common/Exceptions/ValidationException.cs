using FluentValidation.Results;
using ViennaNET.Validation.Rules.ValidationResults;

namespace Ark.SharedLib.Common.Exceptions;

public class ValidationException : ApplicationException
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
                 .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                 .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public ValidationException(ValidationFailure failure)
        : this()
    {
        Errors = new Dictionary<string, string[]>
        {
            [failure.PropertyName] = new[] {failure.ErrorMessage},
        };
    }

    public ValidationException(string message, ViennaNET.Validation.Validators.ValidationResult validationResult) : base(message)
    {
        // TODO there are rule codes and message codes and they could be different
        Errors = validationResult
                 .Results
                 .Where(r => !r.IsValid)
                 .ToDictionary(
                     r => r.RuleIdentity.Code,
                     r => r.Messages.Select(x => x.Error).ToArray()
                 );
    }
    public ValidationException(ViennaNET.Validation.Validators.ValidationResult validationResult)
    {
        Errors = validationResult
                 .Results
                 .Where(r => !r.IsValid)
                 .ToDictionary(
                     r => r.RuleIdentity.Code,
                     r => r.Messages.Select(x => x.Error).ToArray()
                 );
        // TODO there are rule codes and message codes and they could be different
    }

    public ValidationException(string errorPropertyName, string errorMessage)
        : base(errorMessage)
    {
        Errors = new Dictionary<string, string[]>
        {
            [errorPropertyName] = new[] {errorMessage},
        };
    }

    public ValidationException(string errorMessage)
        : base(errorMessage)
    {
        Errors = new Dictionary<string, string[]>
        {
            ["Validation Exception"] = new[] {errorMessage},
        };
    }

    public IDictionary<string, string[]> Errors { get; init; }
}