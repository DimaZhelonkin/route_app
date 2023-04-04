using Ark.SharedLib.Common.Results;
using Newtonsoft.Json;

namespace Ark.SharedLib.Common.Helpers;

public class CacheHelper<TDeserialize, TInvalid>
{
    private CacheHelper(string? cachedStringIsNullMessage,
        string? cachedResponseIsNullMessage,
        string? cachedResponseNotEqualMessage)
    {
        CachedStringIsNullMessage = cachedStringIsNullMessage;
        CachedResponseIsNullMessage = cachedResponseIsNullMessage;
        CachedResponseNotEqualMessage = cachedResponseNotEqualMessage;
        CacheResponse = default;
    }

    public TDeserialize? CacheResponse { get; private set; }

    public string? CachedStringIsNullMessage { get; }

    public string? CachedResponseIsNullMessage { get; }

    public string? CachedResponseNotEqualMessage { get; }

    public static CacheHelper<TDeserialize, TInvalid> Create(
        string? cachedStringIsNullMessage = "",
        string? cachedResponseIsNullMessage = "",
        string? cachedResponseNotEqualMessage = "") =>
        new(cachedStringIsNullMessage, cachedResponseIsNullMessage, cachedResponseNotEqualMessage);

    public Result<CacheHelper<TDeserialize, TInvalid>> CheckCacheValidity(string? cacheString,
        string identifier,
        Severity severity = default,
        string errorCode = ""
    )
    {
        if (cacheString is null)
            return Result.Invalid<CacheHelper<TDeserialize, TInvalid>>(new Error(
                identifier,
                severity: severity,
                errorCode: errorCode,
                errorMessage: CachedStringIsNullMessage ?? "Error Message"
            ));
        CacheResponse = JsonConvert.DeserializeObject<TDeserialize>(cacheString);
        if (CacheResponse is null)
            return Result.Error<CacheHelper<TDeserialize, TInvalid>>(new Error(
                identifier,
                severity: severity,
                errorCode: errorCode,
                errorMessage: CachedResponseIsNullMessage ?? "Error Message"
            ));
        return this;
    }
}