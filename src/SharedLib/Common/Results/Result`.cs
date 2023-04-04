using System.Text.Json.Serialization;

namespace Ark.SharedLib.Common.Results;

public class Result<T> : Result
{
    public Result(T? value)
    {
        Value = value;
        if (Value != null)
            ValueType = Value.GetType();
    }

    public Result(Result<T> result) : this(result.Value, result.SuccessMessage)
    {
        Errors = result.Errors;
        Status = result.Status;
        Errors = result.Errors;
        ValueType = result.ValueType;
    }

    protected Result(Result result) : base(result)
    {
    }

    protected internal Result(T value, string successMessage) : this(value)
    {
        SuccessMessage = successMessage;
    }

    protected Result(ResultStatus status) : base(status)
    {
    }

    public Result(string successMessage) : base(successMessage)
    {
    }

    public T? Value { get; }

    [JsonIgnore]
    public Type? ValueType { get; private set; }


    /// <summary>
    ///     Returns the current value.
    /// </summary>
    /// <returns></returns>
    public object? GetValue() => Value;

    public override string ToString() =>
        string.Format(@"[{0}]({1}): {2}{3}",
            IsSuccess ? "Success" : "Fail",
            Status,
            IsSuccess ? Value?.ToString() ?? "" : MessageWithErrors,
            IsSuccess && Value is not null && ValueType is not null ? $"\n\r of type {ValueType.FullName}" : ""
        );

    public void ClearValueType() => ValueType = null;

    public Result Void() => Result.Create((Result)this);

    public Result<T> Void<T>() => Create<T>(Void());
    // /// <summary>
    // /// Converts PagedInfo into a PagedResult<typeparamref name="T"/>
    // /// </summary>
    // /// <param name="pagedInfo"></param>
    // /// <returns></returns>
    // [Obsolete]
    // public PagedResult<T> ToPagedResult(PagedInfo pagedInfo)
    // {
    //     var pagedResult = new PagedResult<T>(pagedInfo, Data)
    //     {
    //         Status = Status,
    //         SuccessMessage = SuccessMessage,
    //         Errors = Errors,
    //         ValidationErrors = ValidationErrors
    //     };
    //
    //     return pagedResult;
    // }


    public static implicit operator T?(Result<T> result) => result.Value;

    public static implicit operator Result<T>(T value) => Result.Create(value);

    public static Result<T> Create(Result<T> result) => new(result);

    public static new Result<T> Create(Result result) =>
        result is Result<T>
            ? Create<T>(result)
            : new Result<T>(result);

    public static Result<T> Create(T? value) => new(value);

    /// <summary>
    ///     Represents a successful operation and accepts a values as the result of the operation
    /// </summary>
    /// <param name="value">Sets the Value property</param>
    /// <returns>A Result<typeparamref name="T" /></returns>
    public static Result<T> Success(T? value) => new(value);

    /// <summary>
    ///     Represents a successful operation and accepts a values as the result of the operation
    ///     Sets the SuccessMessage property to the provided value
    /// </summary>
    /// <param name="value">Sets the Value property</param>
    /// <param name="successMessage">Sets the SuccessMessage property</param>
    /// <returns>A Result<typeparamref name="T" /></returns>
    public static Result<T> Success(T value, string successMessage) => new(value, successMessage);

    /// <summary>
    ///     Represents a successful operation without return type
    /// </summary>
    /// <param name="successMessage">Sets the SuccessMessage property</param>
    /// <returns>A Result></returns>
    public static new Result<T> SuccessWithMessage(string successMessage) => new(successMessage);

    /// <summary>
    ///     Represents validation errors that prevent the underlying service from completing.
    /// </summary>
    /// <param name="errors">A list of validation errors encountered</param>
    /// <returns>A Result</returns>
    public static new Result<T> Invalid(IEnumerable<Error> errors) =>
        new(ResultStatus.Invalid)
        {
            Errors = errors.ToList(),
        };

    /// <summary>
    ///     Represents validation errors that prevent the underlying service from completing.
    /// </summary>
    /// <param name="errors">A list of validation errors encountered</param>
    /// <returns>A Result</returns>
    public static new Result<T> Invalid(params Error[] errors) => Invalid<T>(errors.ToList());

    /// <summary>
    ///     Represents the situation where a service was unable to find a requested resource.
    /// </summary>
    /// <returns>A Result</returns>
    public static new Result<T> NotFound() => new(ResultStatus.NotFound);

    /// <summary>
    ///     Represents the situation where a service was unable to find a requested resource.
    ///     Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errors">A list of string error messages.</param>
    /// <returns>A Result</returns>
    public static new Result<T> NotFound(params Error[] errors) =>
        new(ResultStatus.NotFound) {Errors = errors.ToList()};

    /// <summary>
    ///     Represents an error that occurred during the execution of the service.
    ///     Error messages may be provided and will be exposed via the Errors property.
    /// </summary>
    /// <param name="errors">A list of string error messages.</param>
    /// <returns>A Result</returns>
    public static new Result<T> Error(params Error[] errors) =>
        new(ResultStatus.Error) {Errors = errors.ToList()};

    /// <summary>
    ///     The parameters to the call were correct, but the user does not have permission to perform some action.
    ///     See also HTTP 403 Forbidden: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
    /// </summary>
    /// <returns>A Result</returns>
    public static new Result<T> Forbidden() => new(ResultStatus.Forbidden);

    /// <summary>
    ///     This is similar to Forbidden, but should be used when the user has not authenticated or has attempted to
    ///     authenticate but failed.
    ///     See also HTTP 401 Unauthorized: https://en.wikipedia.org/wiki/List_of_HTTP_status_codes#4xx_client_errors
    /// </summary>
    /// <returns>A Result</returns>
    public static new Result<T> Unauthorized() => new(ResultStatus.Unauthorized);
}