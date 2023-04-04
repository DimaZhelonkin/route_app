using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Account.Aggregates.Profile.Events;

public class ProfileCreatedEvent : DomainEvent<Guid>
{
    /// <summary>
    ///     ctor for serialization
    /// </summary>
    private ProfileCreatedEvent()
    {
    }

    public ProfileCreatedEvent(Guid identityId, PersonalInformation personalInformation) :
        base(Guid.NewGuid())
    {
        IdentityId = identityId;
        PersonalInformation = personalInformation;
    }

    public ProfileCreatedEvent(Guid aggregateId, long aggregateVersion, Guid identityId,
        PersonalInformation personalInformation)
        : base(aggregateId, aggregateVersion)
    {
        IdentityId = identityId;
        PersonalInformation = personalInformation;
    }

    public Guid IdentityId { get; }
    public PersonalInformation PersonalInformation { get; }

    public override IDomainEvent<Guid> WithAggregate(Guid aggregateId, long aggregateVersion) =>
        new ProfileCreatedEvent(aggregateId, aggregateVersion, IdentityId, PersonalInformation);
}