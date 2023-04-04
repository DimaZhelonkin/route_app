namespace Ark.SharedLib.Common.Exceptions;

public class IllegalStateException : ApplicationException
{
    public IllegalStateException()
    {
    }

    public IllegalStateException(string message) : base(message)
    {
    }

    public IllegalStateException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfNull<T>(T obj, string message)
    {
        if (obj is null)
            throw new IllegalStateException(message);
    }
}