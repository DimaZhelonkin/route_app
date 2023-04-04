using System.Runtime.Serialization;
using FluentValidation;
using FluentValidation.Results;

namespace Ark.Rides.Domain.Aggregates.Ride.Exceptions;

public class RideInvalidException : ValidationException
{
    public RideInvalidException(string message) : base(message)
    {
    }

    public RideInvalidException(string message, IEnumerable<ValidationFailure> errors) : base(message, errors)
    {
    }

    public RideInvalidException(string message, IEnumerable<ValidationFailure> errors, bool appendDefaultMessage) :
        base(message, errors, appendDefaultMessage)
    {
    }

    public RideInvalidException(IEnumerable<ValidationFailure> errors) : base(errors)
    {
    }

    public RideInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}