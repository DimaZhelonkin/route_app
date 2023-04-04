using Ark.Rides.Domain.Enums;
using Ark.SharedLib.Common.Validation;
using ViennaNET.Validation.Rules.FluentRule;

namespace Ark.Rides.Domain.Aggregates.DriverRide.Validations.Rules;

public class DriverRideShouldBeStartedRule : BaseFluentRuleWithResourceManager<DriverRide>
{
    public DriverRideShouldBeStartedRule() : base(InternalCode)
    {
        ForProperty(x => x.Status)
            .Must((x, c) => x is RideStatus.Started or RideStatus.OnTheWay) 
            .WithErrorMessage(InternalCode,
            ResourceManager.GetString($"{nameof(DriverRide.Status)}__Should_Be_Started")!);

        ForProperty(x => x.StartedAt)
            .Must((x, c) => x is not null)
            .WithErrorMessage(InternalCode,
                ResourceManager.GetString($"{nameof(DriverRide.StartedAt)}__Should_Be_Installed")!);
    }

    private static string InternalCode => string.Join(".", nameof(DriverRide), "Should_Be_Started");
}