using Ark.SharedLib.Domain.ValueObjects.Percentages;

namespace Ark.SharedLib.Domain.ValueObjects;

public class MonetaryAmount : ValueObject
{
    public static readonly MonetaryAmount Zero = new(0M);

    public MonetaryAmount(decimal amount)
    {
        Amount = decimal.Round(amount, 2, MidpointRounding.ToEven);
    }

    public decimal Amount { get; }

    public MonetaryAmount Add(MonetaryAmount other) => new(Amount + other.Amount);

    public MonetaryAmount Add(decimal amount) => Add(new MonetaryAmount(amount));

    public MonetaryAmount Subtract(MonetaryAmount other) => new(Amount - other.Amount);

    public MonetaryAmount Subtract(decimal amount) => Subtract(new MonetaryAmount(amount));

    public MonetaryAmount MultiplyByPercent(Percent percent) => new(Amount * percent.Value / 100M);

    public MonetaryAmount MultiplyByPercent(decimal percent) => MultiplyByPercent(new Percent(percent));

    public static MonetaryAmount operator +(MonetaryAmount one, MonetaryAmount two) => one.Add(two);

    public static MonetaryAmount operator -(MonetaryAmount one, MonetaryAmount two) => one.Subtract(two);

    public static MonetaryAmount operator *(MonetaryAmount one, Percent percent) => one.MultiplyByPercent(percent);

    public static bool operator >(MonetaryAmount one, MonetaryAmount two) => one.CompareTo(two) > 0;

    public static bool operator <(MonetaryAmount one, MonetaryAmount two) => one.CompareTo(two) < 0;

    public static bool operator >=(MonetaryAmount one, MonetaryAmount two) => one.CompareTo(two) >= 0;

    public static bool operator <=(MonetaryAmount one, MonetaryAmount two) => one.CompareTo(two) <= 0;

    public static MonetaryAmount Of(decimal value) => new(value);

    public int CompareTo(MonetaryAmount other) => Amount.CompareTo(other.Amount);
}