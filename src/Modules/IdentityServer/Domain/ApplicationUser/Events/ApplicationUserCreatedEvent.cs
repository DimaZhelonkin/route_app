using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Events;
using Newtonsoft.Json;

namespace Ark.IdentityServer.Domain.ApplicationUser.Events;

public class ApplicationUserCreatedEvent : DomainEvent<ApplicationUserId>
{
    public ApplicationUserCreatedEvent(
        string username,
        string email,
        string firstName,
        string lastName,
        string phoneNumber,
        string password,
        DateTimeOffset birthDate)
    {
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Password = password;
        BirthDate = birthDate;
    }

    [JsonConstructor]
    public ApplicationUserCreatedEvent(ApplicationUserId aggregateId, long aggregateVersion,
        string username,
        string email,
        string firstName,
        string lastName,
        string phoneNumber,
        string password,
        DateTimeOffset birthDate) : base(aggregateId, aggregateVersion)
    {
    }

    public string Username { get; }
    public string Email { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string PhoneNumber { get; }
    public string Password { get; }
    public DateTimeOffset BirthDate { get; }


    public override IDomainEvent<ApplicationUserId> WithAggregate(ApplicationUserId aggregateId, long aggregateVersion) =>
        new ApplicationUserCreatedEvent(aggregateId, aggregateVersion,
            Username,
            Email,
            FirstName,
            LastName,
            PhoneNumber,
            Password,
            BirthDate);
}