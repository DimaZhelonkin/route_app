using Ark.Rides.Domain.Aggregates.RideRequest.Events;
using Ark.SharedLib.Domain.Interfaces;
using Ark.SharedLib.Domain.Models.Entities;

namespace Ark.Rides.Domain.Aggregates.RideRequest;

public class RideRequest : AggregateRootBase<RideRequestId>,
    IDomainEventHandler<CreateRideRequestStatusDomainEvent>,
    IDomainEventHandler<RideRequestConfirmedDomainEvent>,
    IDomainEventHandler<RideRequestCanceledDomainEvent>,
    IDomainEventHandler<RideRequestRejectDomainEvent>,
    IDomainEventHandler<RideRequestStatusChangedDomainEvent>
{
    private RideRequestStatus _status;

    /// <summary>
    ///     needs for EF serialization
    /// </summary>
    private RideRequest(RideRequestId id) : base(id)
    {
    }

    private RideRequest(RideRequestId id, DriverRide.DriverRide driverRide,
        PassengerRide.PassengerRide passengerRide) : base(id)
    {
        DriverRide = driverRide;
        PassengerRide = passengerRide;
    }

    public DriverRide.DriverRide DriverRide { get; private set; }
    public PassengerRide.PassengerRide PassengerRide { get; private set; }

    /// <summary>
    /// </summary>
    public RideRequestStatus Status
    {
        get => _status;
        private set => RaiseEvent(new RideRequestStatusChangedDomainEvent(value));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="driverRide"></param>
    /// <param name="passengerRide"></param>
    /// <returns></returns>
    public static RideRequest Create(RideRequestId id, DriverRide.DriverRide driverRide,
        PassengerRide.PassengerRide passengerRide) => new RideRequest(id, driverRide, passengerRide);

    /// <summary>
    /// 
    /// </summary>
    public void Confirm() => RaiseEvent(new RideRequestConfirmedDomainEvent());

    public void Cancel() => RaiseEvent(new RideRequestCanceledDomainEvent());

    public void Reject() => RaiseEvent(new RideRequestRejectDomainEvent());

    #region DomainEventHandlers

    void IDomainEventHandler<CreateRideRequestStatusDomainEvent>.Apply(CreateRideRequestStatusDomainEvent @event)
    {
        DriverRide = @event.DriverRide;
        PassengerRide = @event.PassengerRide;
    }

    void IDomainEventHandler<RideRequestConfirmedDomainEvent>.Apply(RideRequestConfirmedDomainEvent @event) =>
        Status = RideRequestStatus.Confirmed;

    void IDomainEventHandler<RideRequestCanceledDomainEvent>.Apply(RideRequestCanceledDomainEvent @event) =>
        Status = RideRequestStatus.Canceled;

    void IDomainEventHandler<RideRequestRejectDomainEvent>.Apply(RideRequestRejectDomainEvent @event) =>
        Status = RideRequestStatus.Rejected;

    void IDomainEventHandler<RideRequestStatusChangedDomainEvent>.Apply(RideRequestStatusChangedDomainEvent @event) =>
        _status = @event.Status;

    #endregion
}