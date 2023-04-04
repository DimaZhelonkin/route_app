namespace Ark.SharedLib.Common.Results.Exeptions;

public class FailureResultException : ApplicationException
{
    public FailureResultException(string message) : base(message)
    {
    }
}