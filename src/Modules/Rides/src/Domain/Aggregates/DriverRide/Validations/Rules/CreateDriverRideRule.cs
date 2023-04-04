using System.Resources;
using Ark.Rides.Domain.Aggregates.DriverRide.Commands;
using Ark.SharedLib.Common.Validation;
using ViennaNET.Validation.Rules.FluentRule;

namespace Ark.Rides.Domain.Aggregates.DriverRide.Validations.Rules;

public class CreateDriverRideRule : BaseFluentRuleWithResourceManager<CreateDriverRideCommand>
{
    private const string MessageCode1 = $"{nameof(CreateDriverRideCommand.Id)}__Should_Not_Be_Default";
    private const string MessageCode2 = $"{nameof(CreateDriverRideCommand.RouteId)}__Should_Not_Be_Default";
    private const string MessageCode3 = $"{nameof(CreateDriverRideCommand.VehicleId)}__Should_Not_Be_Default";
    private const string MessageCode4 = $"{nameof(CreateDriverRideCommand.Driver)}__Should_Not_Be_Null";

    public CreateDriverRideRule() : base(InternalCode)
    {
        ForProperty(x => x.Id)
            .Must((x, c) => x != default)
            .WithErrorMessage(MessageCode1, ResourceManager.GetString(MessageCode1)!);
        ForProperty(x => x.RouteId)
            .Must((x, c) => x != default)
            .WithErrorMessage(MessageCode2, ResourceManager.GetString(MessageCode2)!);
        ForProperty(x => x.VehicleId)
            .Must((x, c) => x != default)
            .WithErrorMessage(MessageCode3, ResourceManager.GetString(MessageCode3)!);
        ForProperty(x => x.Driver)
            .NotNull()
            .WithErrorMessage(MessageCode4, ResourceManager.GetString(MessageCode4)!);
    }

    private static string InternalCode => string.Join(".", nameof(DriverRide), "Could_Not_Be_Created");

    /// <summary>
    /// </summary>
    /// <returns></returns>
    protected override ResourceManager GetResourceManagerForMessages()
    {
        var assembly = GetType().Assembly;
        var resourcePath = assembly.GetName().Name! + ".Resources." + nameof(DriverRide);
        var resourceManager = new ResourceManager(resourcePath, assembly);
        return resourceManager;
    }
}