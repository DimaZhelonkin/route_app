using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.DriverRide.Events;

/// <summary>
///     Driver Ride Started Event
/// </summary>
public class DriverRideStartedEvent : DomainEvent<DriverRideStartedEvent, DriverRideId>
{
    public DateTimeOffset StartDate { get; }

    /// <summary>
    ///     Needed for serialization
    /// </summary>
    private DriverRideStartedEvent()
    {
    }

    public DriverRideStartedEvent(DateTimeOffset startDate)
    {
        StartDate = startDate;
    }

    public DriverRideStartedEvent(DriverRideId aggregateId, long aggregateVersion, DateTimeOffset startDate)
        : base(aggregateId, aggregateVersion)
    {
        StartDate = startDate;
    }


    public override DriverRideStartedEvent WithAggregate(DriverRideId aggregateId, long aggregateVersion) =>
        new(aggregateId, aggregateVersion, StartDate);
}