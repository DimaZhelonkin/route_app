using Ark.SharedLib.Common.Results;

namespace Ark.SharedLib.Common.Helpers;

public static class CacheHelperExtensions
{
    public static Result<TInvalid> CheckCacheEquals<TDeserialize, TInvalid>(
        this CacheHelper<TDeserialize, TInvalid> source,
        Predicate<TDeserialize> predicate,
        string identifier,
        Severity severity = Severity.Error,
        string errorCode = ""
    )
    {
        if (source.CacheResponse is not null && predicate(source.CacheResponse))
            return Result.Invalid<TInvalid>(new Error(
                identifier,
                source.CachedResponseNotEqualMessage ?? "Error Message",
                errorCode,
                severity));

        return (Result<TInvalid>)Result.Success();
    }
}