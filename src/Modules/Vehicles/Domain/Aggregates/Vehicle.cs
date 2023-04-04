using Ark.SharedLib.Domain.Models.Entities;

namespace Ark.Vehicles.Aggregates;

public class Vehicle : AggregateRootBase<VehicleId>
{
    private Vehicle(VehicleId id, string licensePlate) : base(id)
    {
        LicensePlate = licensePlate;
    }

    private Vehicle(VehicleId id, VehicleOwner owner, string licensePlate) : this(id, licensePlate)
    {
        Owner = owner;
    }

    public string Name { get; set; }
    public uint Capacity { get; set; }

    /// <summary>
    ///     Автомобильный номер
    /// </summary>
    public string LicensePlate { get; init; }

    public bool IsDefault { get; set; } = true;

    public VehicleOwner Owner { get; init; }

    public static Vehicle Create(VehicleId id, VehicleOwner owner, string licensePlate) =>
        new(id, owner, licensePlate);
}