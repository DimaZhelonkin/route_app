using Ark.Rides.Domain.Enums;
using Ark.SharedLib.Common.Validation;
using ViennaNET.Validation.Rules.FluentRule;

namespace Ark.Rides.Domain.Aggregates.DriverRide.Validations.Rules;

/// <summary>
/// 
/// </summary>
public class DriverRideShouldNotBeCancelledRule : BaseFluentRuleWithResourceManager<DriverRide>
{
    public DriverRideShouldNotBeCancelledRule() : base(InternalCode)
    {
        ForProperty(x => x.Status)
            .Must((x, c) => x is not RideStatus.Cancelled)
            .WithErrorMessage(InternalCode,
                ResourceManager.GetString($"{nameof(DriverRide.Status)}__Should_Not_Be_Cancelled")!);

        ForProperty(x => x.CanceledAt)
            .Must((x, c) => x is null)
            .WithErrorMessage(InternalCode,
                ResourceManager.GetString($"{nameof(DriverRide.CanceledAt)}__Should_Not_Be_Installed")!);
    }

    private static string InternalCode => string.Join(".", nameof(DriverRide), "Should_Not_Be_Cancelled");
}