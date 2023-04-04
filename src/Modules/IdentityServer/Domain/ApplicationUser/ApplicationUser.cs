using Ark.IdentityServer.Domain.ApplicationUser.Events;
using Ark.IdentityServer.Domain.ApplicationUser.ValueObjects;
using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Entities;
using Ark.SharedLib.Domain.ValueObjects.Emails;
using Ark.SharedLib.Domain.ValueObjects.PhoneNumbers;

namespace Ark.IdentityServer.Domain.ApplicationUser;

public record ApplicationUserId(Guid Value);

public class ApplicationUser : AggregateRootBase<ApplicationUserId>,
    IDomainEventHandler<ApplicationUserCreatedEvent>
{
    private ApplicationUser(ApplicationUserId id) : base(id)
    {
    }

    private ApplicationUser(ApplicationUserId id,
        string username,
        string email,
        FirstName firstName,
        LastName lastName,
        PhoneNumber phoneNumber,
        Password password,
        DateTimeOffset birthDate) : base(id)
    {
        RaiseEvent(new ApplicationUserCreatedEvent(
            username,
            email,
            firstName,
            lastName,
            phoneNumber,
            password,
            birthDate));
    }

    public IdentityId IdentityId { get; set; }
    public string Username { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Email Email { get; private set; }
    public FirstName FirstName { get; set; }
    public LastName LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public Password Password { get; set; }
    public DateTimeOffset BirthDate { get; set; }

    public Status Status { get; set; }
    public bool IsActive { get; private set; } = true; // TODO change it

    #region IDomainEventHandler<ApplicationUserCreatedEvent> Members

    void IDomainEventHandler<ApplicationUserCreatedEvent>.Apply(ApplicationUserCreatedEvent @event)
    {
        Username = @event.Username;
        PhoneNumber = @event.PhoneNumber;
        Email = @event.Email;
    }

    #endregion

    public static ApplicationUser Create(ApplicationUserId id,
        string username,
        string email,
        FirstName firstName,
        LastName lastName,
        PhoneNumber phoneNumber,
        Password password,
        DateTimeOffset birthDate) =>
        // TODO validate
        new ApplicationUser(id,
            username,
            email,
            firstName,
            lastName,
            phoneNumber,
            password,
            birthDate);

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;

    // public static Result<ApplicationUser> Create(Guid id, string username, string phoneNumber, string email)
    // {
    //     // validate
    //     // new ApplicationUser(id, username, phoneNumber, email);
    // }
}

public enum Status
{
    Deleted,
    Activated,
}