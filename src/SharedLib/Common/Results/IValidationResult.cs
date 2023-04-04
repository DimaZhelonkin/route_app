namespace Ark.SharedLib.Common.Results;

public interface IValidationResult
{
    public static readonly Error Error =
        new("ValidationError", "A validation problem occured.", Severity.Error);

    List<Error> Errors { get; }
}

public class ValidationResult : Result, IValidationResult
{
    public ValidationResult(IEnumerable<Error> errors) : base(ResultStatus.Error)
    {
        Errors = errors.ToList();
    }

    public static ValidationResult WithErrors(Error[] errors) => new ValidationResult(errors);
}

public class ValidationResult<TValue> : Result<TValue>, IValidationResult
{
    public ValidationResult(IEnumerable<Error> errors) : base(ResultStatus.Error)
    {
        Errors = errors.ToList();
    }

    public static ValidationResult WithErrors(Error[] errors) => new ValidationResult(errors);
}