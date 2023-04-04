using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Domain.Aggregates.Driver;
using Ark.Rides.Domain.Aggregates.Passenger;
using Ark.SharedLib.Domain.Interfaces;

namespace Ark.Rides.Domain.Aggregates.RideParticipant;

/// <summary>
///     Base class for <see cref="Driver" />= and <see cref="Passenger" />=
/// </summary>
public abstract class RideParticipant<TId> : User<TId>, IRideParticipant<TId>
    where TId : RideParticipantId
{
    protected RideParticipant(TId id, IdentityId identityId) : base(id, identityId)
    {
    }
}