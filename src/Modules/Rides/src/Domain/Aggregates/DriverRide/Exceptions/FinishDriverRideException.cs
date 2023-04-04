using Ark.Rides.Domain.Aggregates.DriverRide.Validations;
using Ark.SharedLib.Domain.Exceptions;
using ViennaNET.Validation.Validators;

namespace Ark.Rides.Domain.Aggregates.DriverRide.Exceptions;

public sealed class FinishDriverRideException : DomainException<DriverRide>
{
    public FinishDriverRideException() : base(FinishDriverRideRuleSet.RuleCode)
    {
    }

    public FinishDriverRideException(ValidationResult validationResult) : base(FinishDriverRideRuleSet.RuleCode,
        validationResult)
    {
    }
}