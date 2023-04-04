using Ark.Rides.Domain.Aggregates.DriverRide;
using Ark.SharedLib.Application.Abstractions.Repositories;

namespace Ark.Rides.Application.Repositories;

public interface IDriverRidesRepository : IEntityRepository<DriverRide, DriverRideId>
{
}