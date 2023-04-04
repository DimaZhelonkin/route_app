namespace Ark.SharedLib.Domain.ValueObjects.Percentages.Extensions;

public static class PercentExtensions
{
    public static Percent Percent(this int value) => new Percent(value);

    public static Percent Percent(this decimal value) => new Percent(value);
}