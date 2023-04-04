using Ark.SharedLib.Common.CQS.Implementations;
using Ark.Vehicles.Aggregates;

namespace Ark.Rides.Domain.Aggregates.DriverRide.Commands;

public record CreateDriverRideCommand(DriverRideId Id, Driver.Driver Driver, Guid RouteId, VehicleId VehicleId) : CommandResult;