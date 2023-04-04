using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Account.Aggregates.Profile.Events;

public class ProfilePersonalInformationCreatedEvent : DomainEvent<Guid>
{
    /// <summary>
    ///     ctor for serialization
    /// </summary>
    private ProfilePersonalInformationCreatedEvent()
    {
    }

    public ProfilePersonalInformationCreatedEvent(Guid identityId, string firstName, string lastName,
        DateOnly birthDate) :
        base(Guid.NewGuid())
    {
        IdentityId = identityId;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
    }

    public ProfilePersonalInformationCreatedEvent(Guid identityId, string firstName, string lastName,
        string? patronymic,
        DateOnly birthDate) :
        base(Guid.NewGuid())
    {
        IdentityId = identityId;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        BirthDate = birthDate;
    }

    public ProfilePersonalInformationCreatedEvent(Guid aggregateId, long aggregateVersion, Guid identityId,
        string firstName,
        string lastName, string? patronymic, DateOnly birthDate)
        : base(aggregateId, aggregateVersion)
    {
        IdentityId = identityId;
        FirstName = firstName;
        LastName = lastName;
        Patronymic = patronymic;
        BirthDate = birthDate;
    }

    public string FirstName { get; }
    public string LastName { get; }
    public string? Patronymic { get; }
    public DateOnly BirthDate { get; }
    public Guid IdentityId { get; }

    public override IDomainEvent<Guid> WithAggregate(Guid aggregateId, long aggregateVersion) =>
        new ProfilePersonalInformationCreatedEvent(aggregateId, aggregateVersion, IdentityId, FirstName,
            LastName,
            Patronymic, BirthDate);
}