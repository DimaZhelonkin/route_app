using Ark.Rides.Domain.Aggregates.DriverRide.Validations.Rules;
using ViennaNET.Validation.Validators;

namespace Ark.Rides.Domain.Aggregates.DriverRide.Validations;

public class FinishDriverRideRuleSet : BaseValidationRuleSet<DriverRide>
{
    public static string RuleCode = $"{nameof(DriverRide)}__Could_Not_Be_Finished";
    public FinishDriverRideRuleSet()
    {
        SetRule(new DriverRideShouldBeStartedRule()).StopOnFailure();
        SetRule(new DriverRideShouldNotBeFinishedRule()).StopOnFailure();
        SetRule(new DriverRideShouldNotBeCancelledRule()).StopOnFailure();
    }
}