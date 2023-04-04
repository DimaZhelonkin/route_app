using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.SharedLib.Domain.Models.Entities;

namespace Ark.Rides.Domain.Aggregates;

/// <summary>
/// </summary>
/// <typeparam name="TUserId"></typeparam>
public abstract class User<TUserId> : AggregateRootBase<TUserId>
{
    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="identityId"></param>
    public User(TUserId id, IdentityId identityId) : base(id)
    {
        IdentityId = identityId;
    }

    /// <summary>
    /// </summary>
    public IdentityId IdentityId { get; }
}