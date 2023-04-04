using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Domain.Aggregates.RideParticipant;

namespace Ark.Rides.Domain.Aggregates.Passenger;

/// <summary>
/// </summary>
public class Passenger : RideParticipant<PassengerId>
{
    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="identityId"></param>
    private Passenger(PassengerId id, IdentityId identityId) : base(id, identityId)
    {
    }

    public static Passenger Create(PassengerId id, IdentityId identityId) => new(id, identityId);
}