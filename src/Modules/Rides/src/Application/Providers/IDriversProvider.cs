using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Domain.Aggregates.Driver;

namespace Ark.Rides.Application.Providers;

public interface IDriversProvider
{
    Task<Driver?> GetDriver(IdentityId id, CancellationToken cancellationToken = default);
    Task<Driver?> GetDriver(DriverId id, CancellationToken cancellationToken = default);
}