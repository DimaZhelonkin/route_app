using Ardalis.GuardClauses;
using Ark.Account.Aggregates.Profile.Events;
using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Entities;

namespace Ark.Account.Aggregates.Profile;

public class PersonalInformation : AggregateRootBase<Guid>,
    IDomainEventHandler<ProfilePersonalInformationCreatedEvent>
{
    public PersonalInformation(Guid id, Guid identityId, string firstName, string lastName, DateOnly birthDate)
        : base(id)
    {
        Guard.Against.Null(firstName);
        Guard.Against.Null(lastName);
        // TODO check birthdate range

        RaiseEvent(new ProfilePersonalInformationCreatedEvent(identityId, firstName, lastName, birthDate));
    }

    public PersonalInformation(Guid id, Guid identityId, string firstName, string lastName, string? patronymic,
        DateOnly birthDate) : base(id)
    {
        Guard.Against.Null(firstName);
        Guard.Against.Null(lastName);
        // TODO check birthdate range
        RaiseEvent(new ProfilePersonalInformationCreatedEvent(identityId, firstName, lastName, patronymic, birthDate));
    }

    public Guid IdentityId { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    /// <summary>
    ///     Отчество
    /// </summary>
    public string? Patronymic { get; set; }

    public DateOnly BirthDate { get; set; } // TODO add event for changing date of birth 

    #region IDomainEventHandler<ProfilePersonalInformationCreatedEvent> Members

    public void Apply(ProfilePersonalInformationCreatedEvent @event)
    {
        FirstName = @event.FirstName;
        LastName = @event.LastName;
        Patronymic = @event.Patronymic;
        BirthDate = @event.BirthDate;
        IdentityId = @event.IdentityId;
    }

    #endregion
}