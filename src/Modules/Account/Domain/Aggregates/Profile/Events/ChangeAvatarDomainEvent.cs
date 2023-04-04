using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Account.Aggregates.Profile.Events;

public class ChangeAvatarDomainEvent : DomainEvent<Guid>
{
    /// <summary>
    ///     ctor for serialization
    /// </summary>
    private ChangeAvatarDomainEvent()
    {
    }

    public ChangeAvatarDomainEvent(Avatar avatar) :
        base(Guid.NewGuid())
    {
        Avatar = avatar;
    }

    public ChangeAvatarDomainEvent(Guid aggregateId, long aggregateVersion, Avatar avatar)
        : base(aggregateId, aggregateVersion)
    {
        Avatar = avatar;
    }

    public Avatar Avatar { get; }

    public override IDomainEvent<Guid> WithAggregate(Guid aggregateId, long aggregateVersion) =>
        new ChangeAvatarDomainEvent(aggregateId, aggregateVersion, Avatar);
}