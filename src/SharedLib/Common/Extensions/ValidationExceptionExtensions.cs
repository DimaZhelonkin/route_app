using Ark.SharedLib.Common.Exceptions;

namespace Ark.SharedLib.Common.Extensions;

public static class ValidationExceptionExtensions
{
    public static void ThrowWhenNullOrEmpty(this ValidationException exception, string value)
    {
        if (string.IsNullOrEmpty(value))
            throw exception;
    }

    public static void ThrowWhenNullOrEmpty(this ValidationException exception, Guid? value)
    {
        if (value == null || value == Guid.Empty)
            throw exception;
    }

    public static void ThrowWhenNull(this ValidationException exception, object value)
    {
        if (value == null)
            throw exception;
    }
}