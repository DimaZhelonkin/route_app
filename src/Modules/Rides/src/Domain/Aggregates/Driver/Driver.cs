using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Domain.Aggregates.RideParticipant;

namespace Ark.Rides.Domain.Aggregates.Driver;

/// <summary>
/// </summary>
public class Driver : RideParticipant<DriverId>
{
    private Driver(DriverId id, IdentityId identityId) : base(id, identityId)
    {
    }

    /// <summary>
    /// </summary>
    public bool IsLicensed { get; set; }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="identityId"></param>
    /// <returns></returns>
    public static Driver Create(DriverId id, IdentityId identityId) =>
        // TODO validate
        new(id, identityId);
}