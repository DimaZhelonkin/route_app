using Ardalis.Specification;

namespace Ark.IdentityServer.Domain.ApplicationUser.Specifications;

public class GetApplicationUserByUsernameSpec : Specification<ApplicationUser>
{
    public GetApplicationUserByUsernameSpec(string username)
    {
        Query
            .Where(x => x.Username == username);
    }
}