using Ark.Rides.Domain.Aggregates.DriverRide;
using Ark.SharedLib.Common.CQS.Implementations;
using NetTopologySuite.Geometries;

namespace Ark.Rides.Application.Features.Passenger.Commands.SendMatch;

/// <summary>
/// 
/// </summary>
/// <param name="DriverRideId">Driver ride id</param>
/// <param name="StartPoint">Откуда начать поездку</param>
/// <param name="Destination">Конечная точка поездки</param>
public record SendMatchCommand(DriverRideId DriverRideId, Point StartPoint, Point Destination) : CommandResult;