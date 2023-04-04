using Ardalis.SmartEnum;

namespace Ark.IdentityServer.Domain.Roles;

public abstract class RoleEnum : SmartEnum<RoleEnum>
{
    public static readonly RoleEnum User = new UserType();
    public static readonly RoleEnum Admin = new AdminType();
    public static readonly RoleEnum SuperAdmin = new SuperAdminType();

    protected RoleEnum(string name, int value) : base(name, value)
    {
    }

    #region Nested type: AdminType

    private class AdminType : RoleEnum
    {
        public AdminType() : base("Admin", 1)
        {
        }
    }

    #endregion

    #region Nested type: SuperAdminType

    private class SuperAdminType : RoleEnum
    {
        public SuperAdminType() : base("Super Admin", 2)
        {
        }
    }

    #endregion

    #region Nested type: UserType

    private class UserType : RoleEnum
    {
        public UserType() : base("User", 0)
        {
        }
    }

    #endregion
}