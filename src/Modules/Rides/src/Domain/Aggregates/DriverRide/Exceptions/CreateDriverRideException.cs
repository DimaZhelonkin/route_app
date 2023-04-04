using Ark.Rides.Domain.Aggregates.DriverRide.Validations;
using Ark.SharedLib.Domain.Exceptions;
using ViennaNET.Validation.Validators;

namespace Ark.Rides.Domain.Aggregates.DriverRide.Exceptions;

public class CreateDriverRideException : DomainException<DriverRide>
{
    public CreateDriverRideException() : base(CreateDriverRideRuleSet.RuleCode)
    {
    }

    public CreateDriverRideException(ValidationResult validationResult) : base(CreateDriverRideRuleSet.RuleCode,
        validationResult)
    {
    }
}