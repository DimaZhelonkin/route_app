using Ark.SharedLib.Domain.ValueObjects;

namespace Ark.Core.ValueObjects;

public class FIO : ValueObject
{
    public FIO(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    public string FirstName { get; }
    public string LastName { get; }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return FirstName;
        yield return LastName;
    }
}