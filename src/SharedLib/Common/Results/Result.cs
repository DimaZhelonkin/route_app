using System.Text.Json.Serialization;

namespace Ark.SharedLib.Common.Results;

public class Result
{
    protected Result()
    {
    }

    protected Result(string successMessage)
    {
        SuccessMessage = successMessage;
    }

    protected Result(Result result) : this(result.SuccessMessage) // TODO maybe unneeded 
    {
        Errors = result.Errors;
        Status = result.Status;
    }

    protected Result(ResultStatus status)
    {
        Status = status;
    }

// TODO add it to response if no empty as additional field of a response object
    public string SuccessMessage { get; protected set; } = string.Empty;

    [JsonIgnore]
    public string MessageWithErrors => string.Join(", \n\r", Errors.Select(e => e.ToString()));

    public bool IsSuccess => Status == ResultStatus.Ok;

    [JsonIgnore]
    public bool IsFailure => !IsSuccess;

    public ResultStatus Status { get; protected set; } = ResultStatus.Ok;

    // public IEnumerable<string> Errors { get; protected set; } = new List<string>();
    public List<Error> Errors { get; protected set; } = new();

    public override string ToString() =>
        string.Format(@"[{0}]({1}): {2}",
            IsSuccess ? "Success" : "Fail",
            Status,
            IsSuccess ? "" : MessageWithErrors
        );

    public static Result Create(Result result) => new(result);

    /// <summary>
    ///     Creates a new <see cref="Result{TValue}" /> with the specified nullable value and the specified error.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="value">The result value.</param>
    /// <param name="error">The error in case the value is null.</param>
    /// <returns>A new instance of <see cref="Result{TValue}" /> with the specified value or an error.</returns>
    public static Result<T> Create<T>(T? value, Error error)
        where T : class
        => value is null ? Error<T>(error) : Success(value);

    public static Result<T> Create<T>(Result result) => Result<T>.Create(result);

    public static Result<T> Create<T>(Result<T> result) => Result<T>.Create(result);

    public static Result<T> Create<T>(T? value) => Result<T>.Create(value);

    /// <summary>
    ///     Represents a successful operation without return type
    /// </summary>
    /// <returns>A Result</returns>
    public static Result Success() => new();

    /// <summary>
    ///     Represents a successful operation and accepts a values as the result of the operation
    /// </summary>
    /// <param name="value">Sets the Value property</param>
    /// <returns>A Result<typeparamref name="T" /></returns>
    public static Result<T> Success<T>(T? value) => Result<T>.Success(value);

    /// <summary>
    ///     Represents a successful operation and accepts a values as the result of the operation
    ///     Sets the SuccessMessage property to the provided value
    /// </summary>
    /// <param name="value">Sets the Value property</param>
    /// <param name="successMessage">Sets the SuccessMessage property</param>
    /// <returns>A Result<typeparamref name="T" /></returns>
    public static Result<T> Success<T>(T value, string successMessage) => Result<T>.Success(value, successMessage);

    /// <summary>
    ///     Represents a successful operation without return type
    /// </summary>
    /// <param name="successMessage">Sets the SuccessMessage property</param>
    /// <returns>A Result></returns>
    public static Result SuccessWithMessage(string successMessage) => new(successMessage);

    /// <summary>
    ///     Represents a successful operation without return type
    /// </summary>
    /// <param name="successMessage">Sets the SuccessMessage property</param>
    /// <returns>A Result></returns>
    public static Result<T> SuccessWithMessage<T>(string successMessage) =>
        Result<T>.SuccessWithMessage(successMessage);

    /// <summary>
    ///     Represents validation errors that prevent the underlying service from completing.
    /// </summary>
    /// <param name="validationErrors">A list of validation errors encountered</param>
    /// <returns>A Result</returns>
    public static Result Invalid(IEnumerable<Error> validationErrors) =>
        new(ResultStatus.Invalid)
        {
            Errors = validationErrors.ToList(),
        };

    /// <summary>
    ///     Represents validation errors that prevent the underlying service from completing.
    /// </summary>
    /// <param name="validationErrors">A list of validation errors encountered</param>
    /// <returns>A Result</returns>
    public static Result<T> Invalid<T>(IEnumerable<Error> validationErrors) => Result<T>.Invalid(validationErrors);

    /// <summary>
    ///     Represents validation errors that prevent the underlying service from completing.
    /// </summary>
    /// <param name="errors">A list of validation errors encountered</param>
    /// <returns>A Result</returns>
    public static Result Invalid(params Error[] errors) => Invalid(errors.ToList());

    /// <summary>
    ///     Represents validation errors that prevent the underlying service from completing.
    /// </summary>
    /// <param name="errors">A list of validation errors encountered</param>
    /// <returns>A Result</returns>
    public static Result<T> Invalid<T>(params Error[] errors) => Result<T>.Invalid(errors);

    /// <summary>
    ///     Represents the situation where a service was unable to find a requested resource.
    /// </summary>
    /// <returns>A Result</returns>
    public static Result NotFound() => new(ResultStatus.NotFound);

    /// <summary>
    ///     Represents the situation where a service was unable to find a requested resource.
    /// </summary>
    /// <returns>A Result</returns>
    public static Result<T> NotFound<T>() => Result<T>.NotFound();

    /// <summary>
    ///     Represents the situation where a service was unable to find a requested resource.
    ///     Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errors">A list of string error messages.</param>
    /// <returns>A Result</returns>
    public static Result NotFound(params Error[] errors) =>
        new(ResultStatus.NotFound) {Errors = errors.ToList()};

    /// <summary>
    ///     Represents the situation where a service was unable to find a requested resource.
    ///     Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errors">A list of string error messages.</param>
    /// <returns>A Result</returns>
    public static Result<T> NotFound<T>(params Error[] errors) => Result<T>.NotFound(errors);

    /// <summary>
    ///     Represents an error that occurred during the execution of the service.
    ///     Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <returns>A Result</returns>
    public static Result<T> NotFound<T>(string identifier, string errorMessage,
        string? errorCode = null,
        Severity severity = Severity.Error) =>
        NotFound<T>(new Error(identifier, errorMessage, errorCode, severity));

    /// <summary>
    ///     Represents an error that occurred during the execution of the service.
    ///     Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <returns>A Result</returns>
    public static Result Error(string identifier, string errorMessage,
        string? errorCode = null,
        Severity severity = Severity.Error) =>
        Error(new Error(identifier, errorMessage, errorCode, severity));

    /// <summary>
    ///     Represents an error that occurred during the execution of the service.
    ///     Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errors">A list of string error messages.</param>
    /// <returns>A Result</returns>
    public static Result Error(params Error[] errors) => new(ResultStatus.Error) {Errors = errors.ToList()};

    /// <summary>
    ///     Represents an error that occurred during the execution of the service.
    ///     Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errors">A list of string error messages.</param>
    /// <returns>A Result</returns>
    public static Result<T> Error<T>(params Error[] errors) => Result<T>.Error(errors);

    /// <summary>
    ///     Represents an error that occurred during the execution of the service.
    ///     Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <returns>A Result</returns>
    public static Result<T> Error<T>(string identifier, string errorMessage,
        string? errorCode = null,
        Severity severity = Severity.Error) =>
        Error<T>(new Error(identifier, errorMessage, errorCode, severity));

    /// <summary>
    ///     The parameters to the call were correct, but the user does not have permission to perform some action.
    ///     See also HTTP 403 Forbidden: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
    /// </summary>
    /// <returns>A Result</returns>
    public static Result Forbidden() => new(ResultStatus.Forbidden);

    /// <summary>
    ///     The parameters to the call were correct, but the user does not have permission to perform some action.
    ///     See also HTTP 403 Forbidden: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
    /// </summary>
    /// <returns>A Result</returns>
    public static Result<T> Forbidden<T>() => Result<T>.Forbidden();

    /// <summary>
    ///     This is similar to Forbidden, but should be used when the user has not authenticated or has attempted to
    ///     authenticate but failed.
    ///     See also HTTP 401 Unauthorized: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
    /// </summary>
    /// <returns>A Result</returns>
    public static Result Unauthorized() => new(ResultStatus.Unauthorized);

    /// <summary>
    ///     This is similar to Forbidden, but should be used when the user has not authenticated or has attempted to
    ///     authenticate but failed.
    ///     See also HTTP 401 Unauthorized: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
    /// </summary>
    /// <returns>A Result</returns>
    public static Result<T> Unauthorized<T>() => Result<T>.Unauthorized();
}