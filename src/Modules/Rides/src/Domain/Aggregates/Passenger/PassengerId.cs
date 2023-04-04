using Ark.Rides.Domain.Aggregates.RideParticipant;

namespace Ark.Rides.Domain.Aggregates.Passenger;

// ReSharper disable once PartialTypeWithSinglePart
public partial record PassengerId(Guid Value) : RideParticipantId(Value);