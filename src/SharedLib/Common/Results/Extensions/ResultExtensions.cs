using Ark.SharedLib.Common.Results.Exeptions;
using AutoMapper;

namespace Ark.SharedLib.Common.Results.Extensions;

public static class ResultExtensions
{
    /// <summary>
    ///     Transforms a Result's type from a source type to a destination type. If the Result is successful, the func
    ///     parameter is invoked on the Result's source value to map it to a destination type.
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="result"></param>
    /// <param name="valueMapper"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static Result<TDestination> Map<TSource, TDestination>(this Result<TSource> result,
        Func<TSource, TDestination> valueMapper)
    {
        switch (result.Status)
        {
            case ResultStatus.Ok: return valueMapper(result.Value);
            case ResultStatus.NotFound: return Result<TDestination>.NotFound(result.Errors.ToArray());
            case ResultStatus.Unauthorized: return Result<TDestination>.Unauthorized();
            case ResultStatus.Forbidden: return Result<TDestination>.Forbidden();
            case ResultStatus.Invalid: return Result<TDestination>.Invalid(result.Errors);
            case ResultStatus.Error: return Result<TDestination>.Error(result.Errors.ToArray());
            default:
                throw new NotSupportedException($"Result {result.Status} conversion is not supported.");
        }
    }

    /// <summary>
    ///     Transforms a Result's type from a source type to a destination type. If the Result is successful, the func
    ///     parameter is invoked on the Result's source value to map it to a destination type.
    /// </summary>
    /// <typeparam name="TSource">Source value type to use</typeparam>
    /// <typeparam name="TDestination">Destination type</typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static Result<TDestination> Map<TSource, TDestination>(this Result<TSource> result)
    {
        var mapper = ServicesProvider.GetRequiredService<IMapper>();
        switch (result.Status)
        {
            case ResultStatus.Ok: return mapper.Map<TDestination>(result.Value);
            case ResultStatus.NotFound: return Result<TDestination>.NotFound(result.Errors.ToArray());
            case ResultStatus.Unauthorized: return Result<TDestination>.Unauthorized();
            case ResultStatus.Forbidden: return Result<TDestination>.Forbidden();
            case ResultStatus.Invalid: return Result<TDestination>.Invalid(result.Errors);
            case ResultStatus.Error: return Result<TDestination>.Error(result.Errors.ToArray());
            default:
                throw new NotSupportedException($"Result {result.Status} conversion is not supported.");
        }
    }

    /// <summary>
    ///     Transforms a Result's type from a source type to a destination type. If the Result is successful, the func
    ///     parameter is invoked on the Result's source value to map it to a destination type.
    /// </summary>
    /// <typeparam name="TSource">Source value type to use</typeparam>
    /// <typeparam name="TDestination">Destination type</typeparam>
    /// <param name="source">Source Result to map from</param>
    /// <param name="destination">Destination result to map into</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static Result<TDestination> Map<TSource, TDestination>(this Result<TSource> source,
        Result<TDestination> destination)
    {
        var mapper = ServicesProvider.GetRequiredService<IMapper>();
        return mapper.Map(source, destination);
    }


    /// <summary>
    ///     Binds to the result of the function and returns it.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>
    ///     The success result with the bound value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static async Task<Result> Bind<TIn>(this Result<TIn> result, Func<TIn, Task<Result>> func) =>
        result.IsSuccess ? await func(result.Value) : Result.Create(result);

    /// <summary>
    ///     Binds to the result of the function and returns it.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="result">The result.</param>
    /// <param name="func">The bind function.</param>
    /// <returns>
    ///     The success result with the bound value if the current result is a success result, otherwise a failure result.
    /// </returns>
    public static async Task<Result<TOut>>
        Bind<TIn, TOut>(this Result<TIn> result, Func<TIn, Task<Result<TOut>>> func) =>
        result.IsSuccess ? await func(result.Value) : Result.Create<TOut>(result);

    /// <summary>
    ///     Matches the success status of the result to the corresponding functions.
    /// </summary>
    /// <typeparam name="T">The result type.</typeparam>
    /// <param name="resultTask">The result task.</param>
    /// <param name="onSuccess">The on-success function.</param>
    /// <param name="onFailure">The on-failure function.</param>
    /// <returns>
    ///     The result of the on-success function if the result is a success result, otherwise the result of the failure
    ///     result.
    /// </returns>
    public static async Task<T> Match<T>(this Task<Result> resultTask, Func<T> onSuccess, Func<Error, T> onFailure)
    {
        var result = await resultTask;

        return result.IsSuccess ? onSuccess() : onFailure(result.Errors.First());
    }

    /// <summary>
    ///     Matches the success status of the result to the corresponding functions.
    /// </summary>
    /// <typeparam name="TIn">The result type.</typeparam>
    /// <typeparam name="TOut">The output result type.</typeparam>
    /// <param name="resultTask">The result task.</param>
    /// <param name="onSuccess">The on-success function.</param>
    /// <param name="onFailure">The on-failure function.</param>
    /// <returns>
    ///     The result of the on-success function if the result is a success result, otherwise the result of the failure
    ///     result.
    /// </returns>
    public static async Task<TOut> Match<TIn, TOut>(
        this Task<Result<TIn>> resultTask,
        Func<TIn, TOut> onSuccess,
        Func<Error, TOut> onFailure)
    {
        var result = await resultTask;

        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Errors.First());
    }

    public static Result<T> Ensure<T>(this Result<T> result, Func<T, bool> predicate, Error error)
    {
        if (result.IsFailure)
            return result;
        return predicate(result.Value!)
            ? result
            : Result.Error<T>(error);
    }

    public static Result<T> Ensure<T>(this T value,
        params (Func<T, bool> predicate, Error error)[] functions)
    {
        var result = new List<Result<T>>();
        foreach (var (predicate, error) in functions)
            result.Add(Ensure(value, predicate, error));
        return Combine(result.ToArray());
    }

    public static Result<T> Combine<T>(params Result<T>[] results)
    {
        if (results.Any(r => r.IsFailure))
            return Result.Error<T>(
                results
                    .SelectMany(r => r.Errors)
                    // .Where(e => e != Error.None)
                    .Distinct()
                    .ToArray()
            );
        return Result.Success(results[0].Value);
    }

    public static Result<T> ThrowIfFailure<T>(this Result<T> result)
    {
        if (result.IsFailure)
            throw new FailureResultException(result.MessageWithErrors);
        return result;
    }

    public static Result ThrowIfFailure(this Result result)
    {
        if (result.IsFailure)
            throw new FailureResultException(result.MessageWithErrors);
        return result;
    }
}