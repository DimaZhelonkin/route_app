using Ardalis.GuardClauses;
using Ark.Account.Aggregates.Profile.Events;
using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Entities;

namespace Ark.Account.Aggregates.Profile;

public class Profile : AggregateRootBase<Guid>, IDomainEventHandler<ProfileCreatedEvent>,
    IDomainEventHandler<ChangeAvatarDomainEvent>
{
    private Avatar? _avatar;

    // for EF serialization
    // private Profile()
    // {
    // }

    public Profile(Guid id, Guid identityId, PersonalInformation personalInformation) : base(id)
    {
        Guard.Against.Null(identityId);
        RaiseEvent(new ProfileCreatedEvent(identityId, personalInformation));
    }

    public Guid IdentityId { get; private set; }
    public PersonalInformation PersonalInformation { get; private set; }

    public Avatar? Avatar
    {
        get => _avatar;
        set
        {
            Guard.Against.Null(value);
            RaiseEvent(new ChangeAvatarDomainEvent(value));
        }
    }

    public ContactsInformation ContactsInformation { get; set; }

    #region IDomainEventHandler<ChangeAvatarDomainEvent> Members

    void IDomainEventHandler<ChangeAvatarDomainEvent>.Apply(ChangeAvatarDomainEvent @event) => _avatar = @event.Avatar;

    #endregion

    #region IDomainEventHandler<ProfileCreatedEvent> Members

    void IDomainEventHandler<ProfileCreatedEvent>.Apply(ProfileCreatedEvent @event)
    {
        IdentityId = @event.IdentityId;
        PersonalInformation = @event.PersonalInformation;
    }

    #endregion
}