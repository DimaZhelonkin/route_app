using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.PassengerRide.Events;

/// <summary>
///     Passenger Ride Finished Event
/// </summary>
public class PassengerRideFinishedDomainEvent : DomainEvent<PassengerRideFinishedDomainEvent, PassengerRideId>
{
    public DateTimeOffset FinishDate { get; }
    /// <summary>
    ///     Needed for serialization
    /// </summary>
    private PassengerRideFinishedDomainEvent()
    {
    }

    public PassengerRideFinishedDomainEvent(DateTimeOffset finishDate)
    {
        FinishDate = finishDate;
    }

    public PassengerRideFinishedDomainEvent(PassengerRideId aggregateId, long aggregateVersion, DateTimeOffset finishDate)
        : base(aggregateId, aggregateVersion)
    {
        FinishDate = finishDate;
    }


    public override PassengerRideFinishedDomainEvent WithAggregate(PassengerRideId aggregateId, long aggregateVersion) =>
        new(aggregateId, aggregateVersion, FinishDate);
}