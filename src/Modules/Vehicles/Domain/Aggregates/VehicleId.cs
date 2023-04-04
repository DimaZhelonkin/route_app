using Ark.StronglyTypedIds;

namespace Ark.Vehicles.Aggregates;

public partial record VehicleId(Guid Value) : StronglyTypedId<Guid>(Value);