using Ark.StronglyTypedIds;

namespace Ark.Rides.Domain.Aggregates.RideParticipant;

/// <summary>
/// </summary>
public partial record RideParticipantId(Guid Value) : StronglyTypedId<Guid>(Value); 