using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.SharedLib.Application.Abstractions.Repositories;

namespace Ark.IdentityServer.DomainServices.Repositories;

public interface IApplicationUserRepository : IEntityRepository<ApplicationUser, ApplicationUserId>
{
}