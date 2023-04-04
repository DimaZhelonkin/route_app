using Ark.Rides.Domain.Aggregates.RideRequest;
using Ark.SharedLib.Application.Abstractions.Repositories;

namespace Ark.Rides.Application.Repositories;

public interface IRideRequestsRepository : IEntityRepository<RideRequest, RideRequestId>
{
}