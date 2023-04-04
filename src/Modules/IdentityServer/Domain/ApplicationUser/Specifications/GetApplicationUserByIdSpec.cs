using Ardalis.Specification;

namespace Ark.IdentityServer.Domain.ApplicationUser.Specifications;

public class GetApplicationUserByIdSpec : Specification<ApplicationUser>
{
    public GetApplicationUserByIdSpec(ApplicationUserId id)
    {
        Query
            .Where(x => x.Id == id);
    }
}