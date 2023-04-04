namespace Ark.SharedLib.Application.Extensions;

public static class TimeSpanExtensions
{
    public static string ToReadableString(this TimeSpan span)
    {
        var formatted = string.Format("{0}{1}{2}{3}{4}",
            span.Duration().Days > 0
                ? string.Format("{0:0} d, ", span.Days, span.Days == 1 ? string.Empty : "s")
                : string.Empty,
            span.Duration().Hours > 0
                ? string.Format("{0:0} h, ", span.Hours, span.Hours == 1 ? string.Empty : "s")
                : string.Empty,
            span.Duration().Minutes > 0
                ? string.Format("{0:0} min, ", span.Minutes, span.Minutes == 1 ? string.Empty : "s")
                : string.Empty,
            span.Duration().Seconds > 0
                ? string.Format("{0:0} sec, ", span.Seconds, span.Seconds == 1 ? string.Empty : "s")
                : string.Empty,
            span.Duration().Milliseconds > 0
                ? string.Format("{0:0} ms", span.Milliseconds, span.Seconds == 1 ? string.Empty : "s")
                : string.Empty);

        if (formatted.EndsWith(", "))
            formatted = formatted[..^2];

        if (string.IsNullOrEmpty(formatted))
            formatted = "0s";

        return formatted;
    }
}