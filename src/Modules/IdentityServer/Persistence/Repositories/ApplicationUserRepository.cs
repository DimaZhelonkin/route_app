using Ardalis.Specification;
using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.IdentityServer.DomainServices.Repositories;
using Ark.SharedLib.Persistence.Repositories;

namespace Ark.IdentityServer.Persistence.Repositories;

public class ApplicationUserRepository : EFRepository<ApplicationUser, ApplicationUserId, IdentityServerDbContext>,
    IApplicationUserRepository
{
    public ApplicationUserRepository(IdentityServerDbContext context,
        ISpecificationEvaluator? specificationEvaluator = null)
        : base(
            context, specificationEvaluator)
    {
    }
}