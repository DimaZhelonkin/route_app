using Ark.IdentityServer.Domain.Roles;

namespace Ark.IdentityServer.Application.Localisation;

/// <summary>
///     Provides various localization keys for resolving to localized resources
/// </summary>
public interface ILocalizationKeyProvider
{
    string GetRoleLocalizationKey(RoleEnum role);
}