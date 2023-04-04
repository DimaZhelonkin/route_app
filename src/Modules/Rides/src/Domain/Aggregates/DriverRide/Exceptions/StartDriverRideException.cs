using Ark.Rides.Domain.Aggregates.DriverRide.Validations;
using Ark.SharedLib.Domain.Exceptions;
using ViennaNET.Validation.Validators;

namespace Ark.Rides.Domain.Aggregates.DriverRide.Exceptions;

public class StartDriverRideException : DomainException<DriverRide>
{
    public StartDriverRideException() : base(StartDriverRideRuleSet.RuleCode)
    {
    }

    public StartDriverRideException(ValidationResult validationResult) : base(StartDriverRideRuleSet.RuleCode,
        validationResult)
    {
    }
}