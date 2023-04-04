namespace Ark.SharedLib.Domain.ValueObjects.Percentages;

// source: https://github.com/asc-lab/better-code-with-ddd/blob/ef_core/LoanApplication.TacticalDdd/LoanApplication.TacticalDdd/DomainModel/Percent.cs
public class Percent : ValueObject
{
    public static readonly Percent Zero = new(0M);

    public Percent(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Percent value cannot be negative");

        Value = value;
    }

    protected Percent() { } // EF Core
    public decimal Value { get; }

    public static bool operator >(Percent one, Percent two) => one.CompareTo(two) > 0;

    public static bool operator <(Percent one, Percent two) => one.CompareTo(two) < 0;

    public static bool operator >=(Percent one, Percent two) => one.CompareTo(two) >= 0;

    public static bool operator <=(Percent one, Percent two) => one.CompareTo(two) <= 0;

    private int CompareTo(Percent other) => Value.CompareTo(other.Value);

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}