using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.DriverRide.Events;

/// <summary>
///     Driver Ride Finished Event
/// </summary>
public class DriverRideFinishedEvent : DomainEvent<DriverRideFinishedEvent, DriverRideId>
{
    public DateTimeOffset FinishDate { get; }
    /// <summary>
    ///     Needed for serialization
    /// </summary>
    private DriverRideFinishedEvent()
    {
    }

    public DriverRideFinishedEvent(DateTimeOffset finishDate)
    {
        FinishDate = finishDate;
    }

    public DriverRideFinishedEvent(DriverRideId aggregateId, long aggregateVersion, DateTimeOffset finishDate)
        : base(aggregateId, aggregateVersion)
    {
        FinishDate = finishDate;
    }


    public override DriverRideFinishedEvent WithAggregate(DriverRideId aggregateId, long aggregateVersion) =>
        new(aggregateId, aggregateVersion, FinishDate);
}