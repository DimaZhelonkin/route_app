using Ark.Routing.Aggregates;
using Ark.Routing.Contracts;
using Ark.Routing.Features.GetRouteQuery;
using Ark.Routing.HttpClients.VkClient;
using Ark.Routing.Services.Options;
using Ark.SharedLib.Common.Helpers;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Ark.Routing.Services;

public class CachedRoutingProvider : IRoutingProvider
{
    private readonly IRoutingProvider _routingProvider;
    private readonly IVkRoutingClient _vkRoutingClient;
    private readonly IDistributedCache _distributedCache;
    private readonly IOptions<RoutingOptions> _options;


    public CachedRoutingProvider(IRoutingProvider routingProvider, IVkRoutingClient vkRoutingClient,
        IDistributedCache distributedCache, IOptions<RoutingOptions> options)
    {
        _routingProvider = routingProvider;
        _vkRoutingClient = vkRoutingClient;
        _distributedCache = distributedCache;
        _options = options;
    }

    public async Task<Result<Route>> GetRouteAsync(GetRouteQuery query, CancellationToken cancellationToken = default)
    {
        var key = $"InitCallLogin_{query.Id}";
        var cacheString = await _distributedCache.GetStringAsync(key, cancellationToken);
        var cacheResult = CacheHelper<GetRouteQuery, Route>
            .Create("Exception",
                "Exception")
            .CheckCacheValidity(cacheString, query.Id).Value!
            .CheckCacheEquals(
                x => query.Id != x.Id,
                identifier: $"{query.Id}:Equals");
        if (cacheResult.Status is ResultStatus.Invalid or ResultStatus.Error) return cacheResult;
        var route = await _routingProvider.GetRouteAsync(query, cancellationToken);
        await _distributedCache.SetStringAsync(key, JsonConvert.SerializeObject(route),
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30),
            },
            cancellationToken);
        return Result.Success(route.Value);
    }
}