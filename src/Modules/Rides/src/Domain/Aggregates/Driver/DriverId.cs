using Ark.Rides.Domain.Aggregates.RideParticipant;

namespace Ark.Rides.Domain.Aggregates.Driver;

/// <summary>
/// </summary>
/// <param name="Value"></param>
public partial record DriverId(Guid Value) : RideParticipantId(Value);