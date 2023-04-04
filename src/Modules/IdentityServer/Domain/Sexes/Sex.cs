using Ark.SharedLib.Domain.ValueObjects;

namespace Ark.IdentityServer.Domain.Sexes;

public class Sex : ValueObject
{
    private readonly SexEnum _sex;

    public Sex(string value)
    {
        Value = value;
    }

    public Sex(SexEnum value)
    {
        Value = value.Name;
    }

    protected Sex() { } // EF Core

    public string Value
    {
        get => _sex.Name;
        private init
        {
            if (string.IsNullOrEmpty(value))
                value = SexEnum.Unknown.Name;
            if (value.Trim().Equals("m", StringComparison.InvariantCultureIgnoreCase))
                value = SexEnum.Male.Name;
            if (value.Trim().Equals("f", StringComparison.InvariantCultureIgnoreCase))
                value = SexEnum.Female.Name;

            if (!SexEnum.TryFromName(value, true, out var parsed))
                parsed = SexEnum.Unknown;

            _sex = parsed;
        }
    }

    public static Sex Of(string value) => new(value);
    public static implicit operator string(Sex value) => value.Value;
    public static List<string> ListNames() => SexEnum.List.Select(x => x.Name).ToList();

    public static Sex Unknown() => new(SexEnum.Unknown.Name);
    public static Sex Male() => new(SexEnum.Male.Name);
    public static Sex Female() => new(SexEnum.Female.Name);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}