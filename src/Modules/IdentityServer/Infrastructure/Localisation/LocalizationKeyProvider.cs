using Ark.IdentityServer.Application.Localisation;
using Ark.IdentityServer.Domain.Roles;
using Ark.Infrastructure.Resources;

namespace Ark.IdentityServer.Infrastructure.Localisation;

public class LocalizationKeyProvider : ILocalizationKeyProvider
{
    #region ILocalizationKeyProvider Members

    public string GetRoleLocalizationKey(RoleEnum role) =>
        role switch
        {
            _ when role.Equals(RoleEnum.User) => ResourceKeys.Roles_User,
            _ when role.Equals(RoleEnum.Admin) => ResourceKeys.Roles_Admin,
            _ when role.Equals(RoleEnum.SuperAdmin) => ResourceKeys.Roles_SuperAdmin,
            _ => role.ToString(),
        };

    #endregion
}