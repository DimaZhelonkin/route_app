using Ark.SharedLib.Common.Exceptions;
using Ark.SharedLib.Domain.ValueObjects;

namespace Ark.IdentityServer.Domain.Roles;

public class Role : ValueObject
{
    private readonly RoleEnum _role;

    public Role(string value)
    {
        Value = value;
    }

    public Role(RoleEnum value)
    {
        Value = value.Name;
    }

    protected Role() { } // EF Core

    public string Value
    {
        get => _role.Name;
        private init
        {
            if (!RoleEnum.TryFromName(value, true, out var parsed))
                throw new InvalidSmartEnumPropertyName(nameof(Value), value);

            _role = parsed;
        }
    }

    public static Role Of(string value) => new Role(value);

    public static implicit operator string(Role value) => value.Value;

    public static List<string> ListNames() => RoleEnum.List.Select(x => x.Name).ToList();

    public static Role User() => new Role(RoleEnum.User.Name);

    public static Role SuperAdmin() => new Role(RoleEnum.SuperAdmin.Name);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}