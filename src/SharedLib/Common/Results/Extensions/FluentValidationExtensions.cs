using FluentValidation.Results;

namespace Ark.SharedLib.Common.Results.Extensions;

public static class FluentValidationExtensions
{
    public static List<Error> AsErrors(this FluentValidation.Results.ValidationResult valResult) =>
        valResult.Errors
                 .Select(AsError)
                 .ToList();

    public static Error AsError(this ValidationFailure validationFailure) =>
        new Error(
            validationFailure.PropertyName,
            validationFailure.ErrorMessage,
            validationFailure.ErrorCode,
            FromSeverity(validationFailure.Severity)
        );

    public static Severity FromSeverity(this FluentValidation.Severity severity) =>
        severity switch
        {
            FluentValidation.Severity.Error => Severity.Error,
            FluentValidation.Severity.Warning => Severity.Warning,
            FluentValidation.Severity.Info => Severity.Info,
            _ => throw new ArgumentOutOfRangeException(nameof(severity), "Unexpected Severity"),
        };
}