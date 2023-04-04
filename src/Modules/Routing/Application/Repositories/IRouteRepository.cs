using Ark.Routing.Aggregates;
using Ark.SharedLib.Application.Abstractions.Repositories;

namespace Ark.Routing.Repositories;

public interface IRouteRepository : IEntityRepository<Route, Guid>
{
}