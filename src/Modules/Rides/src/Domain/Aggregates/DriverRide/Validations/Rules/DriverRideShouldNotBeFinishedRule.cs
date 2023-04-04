using Ark.Rides.Domain.Enums;
using Ark.SharedLib.Common.Validation;
using ViennaNET.Validation.Rules.FluentRule;

namespace Ark.Rides.Domain.Aggregates.DriverRide.Validations.Rules;

public class DriverRideShouldNotBeFinishedRule : BaseFluentRuleWithResourceManager<DriverRide>
{
    public DriverRideShouldNotBeFinishedRule() : base(InternalCode)
    {
        ForProperty(x => x.Status)
            .Must((x, c) => x is not RideStatus.Finished)
            .WithErrorMessage(InternalCode,
                ResourceManager.GetString($"{nameof(DriverRide.Status)}__Should_Not_Be_Finished")!);

        ForProperty(x => x.FinishedAt)
            .Must((x, c) => x is null)
            .WithErrorMessage(InternalCode,
                ResourceManager.GetString($"{nameof(DriverRide.FinishedAt)}__Should_Not_Be_Installed")!);
    }

    private static string InternalCode => string.Join(".", nameof(DriverRide), "Should_Not_Be_Finished");
}