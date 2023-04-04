using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.SharedLib.Domain.Models.Entities;

namespace Ark.Account.Aggregates;

public class ContactsInformation : AggregateRootBase<Guid>
{
    public ContactsInformation(Guid id, IdentityId identityId) : base(id)
    {
        IdentityId = identityId;
    }

    public IdentityId IdentityId { get; }

    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? TelegramNickname { get; set; }
}