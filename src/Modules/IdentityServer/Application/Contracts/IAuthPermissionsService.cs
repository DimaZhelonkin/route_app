namespace Ark.IdentityServer.Application.Contracts;

public interface IAuthPermissionsService
{
    Task<bool> HasPermissionsAsync(Guid countryId, string token, CancellationToken cancellationToken = default);
}