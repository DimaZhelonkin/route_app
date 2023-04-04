using Ark.StronglyTypedIds;

namespace Ark.Rides.Domain.Aggregates.RideRequest;

public record RideRequestId(Guid Value) : StronglyTypedId<Guid>(Value);