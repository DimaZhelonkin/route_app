using System.Resources;
using Ark.Rides.Domain.Aggregates.DriverRide.Commands;
using Ark.Rides.Domain.Aggregates.DriverRide.Validations.Rules;
using Ark.SharedLib.Common.Validation;

namespace Ark.Rides.Domain.Aggregates.DriverRide.Validations;

/// <summary>
/// </summary>
public class CreateDriverRideRuleSet : BaseValidationRuleSetWithResourceManager<CreateDriverRideCommand>
{
    public static string RuleCode = $"{nameof(DriverRide)}__Could_Not_Be_Created";

    /// <summary>
    /// </summary>
    public CreateDriverRideRuleSet()
    {
        SetRule(new CreateDriverRideRule());
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    protected override ResourceManager GetResourceManagerForMessages()
    {
        var assembly =  GetType().Assembly;
        var resourcePath = assembly.GetName().Name! + ".Resources." + nameof(DriverRide);
        var resourceManager = new ResourceManager(resourcePath, assembly);
        return resourceManager;
    }
}