using System.Collections.Concurrent;
using Ark.SharedLib.Application.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Ark.Infrastructure.Caching;

public class CacheService : ICacheService
{
    private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    #region ICacheService Members

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        var cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);
        if (cachedValue is null)
            return null;
        var value = JsonConvert.DeserializeObject<T>(cachedValue);
        return value;
    }

    public async Task<T> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken cancellationToken = default)
        where T : class
    {
        var cachedValue = await GetAsync<T>(key, cancellationToken);
        if (cachedValue is not null)
            return cachedValue;

        cachedValue = await factory();
        await SetAsync(key, cachedValue, cancellationToken);

        return cachedValue;
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
    {
        var cachedValue = JsonConvert.SerializeObject(value);
        await _distributedCache.SetStringAsync(key, cachedValue, cancellationToken);

        CacheKeys.TryAdd(key, true);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _distributedCache.RefreshAsync(key, cancellationToken);
        CacheKeys.TryRemove(key, out _);
    }

    public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
    {
        var tasks = CacheKeys.Keys
                             .Where(key => key.StartsWith(prefixKey))
                             .Select(k => RemoveAsync(k, cancellationToken));
        await Task.WhenAll(tasks);
    }

    #endregion
}