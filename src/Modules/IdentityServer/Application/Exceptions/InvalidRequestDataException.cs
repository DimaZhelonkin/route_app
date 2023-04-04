using Ark.SharedLib.Common.Exceptions;

namespace Ark.IdentityServer.Application.Exceptions;

public class InvalidRequestDataException : NotFoundException
{
    public InvalidRequestDataException(string message) : base(message)
    {
    }
}