using Ark.SharedLib.Domain.Models.Events;

namespace Ark.Rides.Domain.Aggregates.PassengerRide.Events;

/// <summary>
///     Driver Ride Started Event
/// </summary>
public class PassengerRideStartedDomainEvent : DomainEvent<PassengerRideStartedDomainEvent, PassengerRideId>
{
    public DateTimeOffset StartDate { get; }

    /// <summary>
    ///     Needed for serialization
    /// </summary>
    private PassengerRideStartedDomainEvent()
    {
    }

    public PassengerRideStartedDomainEvent(DateTimeOffset startDate)
    {
        StartDate = startDate;
    }

    public PassengerRideStartedDomainEvent(PassengerRideId aggregateId, long aggregateVersion, DateTimeOffset startDate)
        : base(aggregateId, aggregateVersion)
    {
        StartDate = startDate;
    }


    public override PassengerRideStartedDomainEvent WithAggregate(PassengerRideId aggregateId, long aggregateVersion) =>
        new(aggregateId, aggregateVersion, StartDate);
}