using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Domain.Aggregates.Driver;
using Ark.Rides.Domain.Aggregates.DriverRide;
using Ark.Rides.Domain.Aggregates.DriverRide.Commands;
using Ark.Rides.Domain.Aggregates.DriverRide.Events;
using Ark.Rides.Domain.Aggregates.DriverRide.Exceptions;
using Ark.SharedLib.Domain.Interfaces;
using Ark.Vehicles.Aggregates;
using FluentAssertions;

namespace Ark.Rides.UnitTests.Domain.DriverRideTests;

public class CreateDriverRideTests
{
    [Fact]
    public void Create_ShouldRaiseDriverRideCreatedEvent_WhenParametersAreValid()
    {
        // Arrange
        var driverRideId = new DriverRideId(Guid.NewGuid());
        var driver = TestDrivers.Simple();
        var routeId = Guid.NewGuid();
        var vehicleId = new VehicleId(Guid.NewGuid());
        var createCommand = new CreateDriverRideCommand(driverRideId, driver, routeId, vehicleId);
        // Act
        var driverRide = DriverRide.Create(createCommand);

        // Assert
        ((IAggregateRoot)driverRide).GetDomainEvents().Should().ContainItemsAssignableTo<DriverRideCreatedEvent>();
    }

    [Fact]
    public void Create_ShouldThrowCreateDriverRideException_WhenDriverRideIdIsDefault()
    {
        // Arrange
        var driverRideId = default(DriverRideId);
        var driver = TestDrivers.Simple();
        var routeId = Guid.NewGuid();
        var vehicleId = new VehicleId(Guid.NewGuid());
        var createCommand = new CreateDriverRideCommand(driverRideId!, driver, routeId, vehicleId);
        // Act
        Action act = () => DriverRide.Create(createCommand);
        // Assert
        act.Should().Throw<CreateDriverRideException>();
    }

    [Fact]
    public void Create_ShouldThrowCreateDriverRideException_WhenDriverIsNull()
    {
        // Arrange
        var driverRideId = new DriverRideId(Guid.NewGuid());
        var routeId = Guid.NewGuid();
        var vehicleId = new VehicleId(Guid.NewGuid());
        var createCommand = new CreateDriverRideCommand(driverRideId, null!, routeId, vehicleId);
        // Act
        Action act = () => DriverRide.Create(createCommand);
        // Assert
        act.Should().Throw<CreateDriverRideException>();
    }

    [Fact]
    public void Create_ShouldThrowCreateDriverRideException_WhenVehicleIdIsDefault()
    {
        // Arrange
        var driverRideId = new DriverRideId(Guid.NewGuid());
        var driver = TestDrivers.Simple();
        var routeId = Guid.NewGuid();
        var vehicleId = default(VehicleId);
        var createCommand = new CreateDriverRideCommand(driverRideId, driver, routeId, vehicleId!);
        // Act
        Action act = () => DriverRide.Create(createCommand);
        // Assert
        act.Should().Throw<CreateDriverRideException>();
    }

    [Fact]
    public void Create_ShouldThrowCreateDriverRideException_WhenRouteIdIsDefault()
    {
        // Arrange
        var driverRideId = new DriverRideId(Guid.NewGuid());
        var driver = TestDrivers.Simple();
        var routeId = default(Guid);
        var vehicleId = new VehicleId(Guid.NewGuid());
        var createCommand = new CreateDriverRideCommand(driverRideId, driver, routeId, vehicleId);
        // Act
        Action act = () => DriverRide.Create(createCommand);
        // Assert
        act.Should().Throw<CreateDriverRideException>();
    }
}