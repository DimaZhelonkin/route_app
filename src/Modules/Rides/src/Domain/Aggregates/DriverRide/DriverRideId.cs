using Ark.Rides.Domain.Aggregates.Ride;

namespace Ark.Rides.Domain.Aggregates.DriverRide;

public partial record DriverRideId(Guid Value) : RideId(Value);