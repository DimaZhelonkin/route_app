using Ark.Rides.Domain.Aggregates.Driver;
using Ark.Rides.Domain.Aggregates.DriverRide;

namespace Ark.Rides.Application.Providers;

public interface IDriverRidesProvider
{
    Task<DriverRide?> GetById(DriverRideId id, CancellationToken cancellationToken = default);
}