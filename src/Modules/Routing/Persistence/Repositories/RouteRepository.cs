using Ardalis.Specification;
using Ark.Routing.Aggregates;
using Ark.SharedLib.Persistence.Repositories;

namespace Ark.Routing.Repositories;

public class RouteRepository : EFRepository<Route, Guid, RoutingDbContext>, IRouteRepository
{
    public RouteRepository(RoutingDbContext context, ISpecificationEvaluator? specificationEvaluator = null) : base(
        context, specificationEvaluator)
    {
    }
}