using Ardalis.Specification;
using Ark.SharedLib.Domain.ValueObjects.PhoneNumbers;

namespace Ark.IdentityServer.Domain.ApplicationUser.Specifications;

public sealed class GetApplicationUserByPhoneNumberSpec : Specification<ApplicationUser>
{
    public GetApplicationUserByPhoneNumberSpec(PhoneNumber phoneNumber)
    {
        // TODO change it to equal
        Query
            .Where(x => x.PhoneNumber.Value == phoneNumber.Value);
    }
}