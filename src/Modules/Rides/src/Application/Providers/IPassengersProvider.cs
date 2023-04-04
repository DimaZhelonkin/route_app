using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Domain.Aggregates.Passenger;

namespace Ark.Rides.Application.Providers;

public interface IPassengersProvider
{
    Task<Passenger?> GetPassengerByIdentityId(IdentityId identityId, CancellationToken cancellationToken);
}