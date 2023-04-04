using Ark.Routing.Contracts;
using Ark.Routing.Repositories;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;

namespace Ark.Routing.Features.GetRouteQuery;

public class GetRouteQueryHandler : QueryHandler<GetRouteQuery, GetRouteResult>
{
    private readonly IRouteRepository _routeRepository;
    private readonly IRoutingProvider _routingProvider;

    public GetRouteQueryHandler(IRouteRepository routeRepository, IRoutingProvider routingProvider)
    {
        _routeRepository = routeRepository;
        _routingProvider = routingProvider;
    }

    public override async Task<Result<GetRouteResult>> Handle(GetRouteQuery query,
        CancellationToken cancellationToken = default)
    {
        // TODO if id of the route is provided, try to get route from db, else it has to find a route from vk and save found route
        var route = await _routingProvider.GetRouteAsync(query, cancellationToken);
        var addedRoute = await _routeRepository.AddAsync(route.Value!, cancellationToken);

        // вот эта штука валится с той самой ошибкой, что не может json переводить бесконечность)
        await _routeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        var a = addedRoute.Current.Coordinates.ToString();
        var result = Result.Success(new GetRouteResult {Route = addedRoute.Current.ToText()});
        return result;
    }
}