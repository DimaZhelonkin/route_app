using Ark.SharedLib.Domain.ValueObjects;

namespace Ark.Account.Aggregates.Profile;

public class Avatar : ValueObject
{
    // for EF serialization
    private Avatar()
    {
    }

    public Avatar(string avatarUrl)
    {
        AvatarUrl = avatarUrl;
    }

    public string AvatarUrl { get; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return AvatarUrl;
    }
}