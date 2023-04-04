namespace Ark.SharedLib.Common.Results;

public class Error
{
    public Error(string identifier, string errorMessage,
        string? errorCode = null,
        Severity severity = Severity.Error) : this(identifier, errorMessage, severity)
    {
        ErrorCode = errorCode;
    }

    public Error(string identifier, string errorMessage) : this(identifier, errorMessage, Severity.Error)
    {
        ErrorMessage = errorMessage;
        Identifier = identifier;
    }

    public Error(string identifier, string errorMessage,
        Severity severity = Severity.Error)
    {
        Severity = severity;
        ErrorMessage = errorMessage;
        Identifier = identifier;
    }

    public string Identifier { get; init; }
    public string ErrorMessage { get; init; }
    public string? ErrorCode { get; init; } = string.Empty;
    public Severity Severity { get; init; }


    public override string ToString() =>
        string.Format(@"[{0}{1}]: {2} Message: {3}",
            Severity,
            ErrorCode is null ? "" : $" - {ErrorCode}",
            string.IsNullOrWhiteSpace(Identifier) ? "" : $"({Identifier})",
            ErrorMessage);

    public static implicit operator string(Error error) => error.ToString();
}