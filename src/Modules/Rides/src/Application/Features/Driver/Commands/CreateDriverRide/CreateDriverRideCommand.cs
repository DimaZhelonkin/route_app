using Ark.SharedLib.Common.CQS.Implementations;
using Ark.Vehicles.Aggregates;
using NetTopologySuite.Geometries;

namespace Ark.Rides.Application.Features.Driver.Commands.CreateDriverRide;

public record CreateDriverRideCommand(
    Point StartPoint, 
    Point Destination,
    uint Capacity, 
    decimal? MinPrice = default,
    VehicleId? VehicleId = default) : CommandResult
{
    /// <summary>
    ///     Откуда начать поездку
    /// </summary>
    public Point StartPoint { get; init; } = StartPoint;

    /// <summary>
    ///     Конечная точка поездки
    /// </summary>
    public Point Destination { get; init; } = Destination;

    /// <summary>
    ///     Количество свободных мест на время поездки
    /// </summary>
    public uint Capacity { get; init; } = Capacity;

    /// <summary>
    ///     Дата начала поездки
    ///     Default: Now
    /// </summary>
    public DateTimeOffset? StartDate { get; init; } = DateTimeOffset.UtcNow;

    /// <summary>
    ///     Минимальная сумма поездки
    /// </summary>
    public decimal? MinPrice { get; init; } = MinPrice;

    /// <summary>
    ///     Выбранная машина для поездки
    ///     По умолчанию: выбранная машина в профиле
    /// </summary>
    public VehicleId? VehicleId { get; init; } = VehicleId;
}