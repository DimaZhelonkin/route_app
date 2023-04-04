using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Domain.Aggregates.Driver;
using Ark.Rides.Domain.Aggregates.DriverRide;
using Ark.Rides.Domain.Aggregates.DriverRide.Commands;
using Ark.Rides.Domain.Aggregates.DriverRide.Events;
using Ark.Rides.Domain.Aggregates.DriverRide.Exceptions;
using Ark.Rides.Domain.Enums;
using Ark.SharedLib.Domain.Interfaces;
using Ark.Vehicles.Aggregates;
using FluentAssertions;

namespace Ark.Rides.UnitTests.Domain.DriverRideTests;

public class StartDriverRideTests
{
    public StartDriverRideTests()
    {
        DriverRide = CreateFixtureDriverRide();
    }

    public DriverRide DriverRide { get; init; }

    private static DriverRide CreateFixtureDriverRide()
    {
        var driverRideId = new DriverRideId(Guid.NewGuid());
        var driverId = new DriverId(Guid.NewGuid());
        var driverIdentityId = new IdentityId(driverId.ToString());

        var driver = Driver.Create(driverId, driverIdentityId);
        var routeId = Guid.NewGuid();
        var vehicleId = new VehicleId(Guid.NewGuid());
        var createCommand = new CreateDriverRideCommand(driverRideId, driver, routeId, vehicleId);
        var driverRide = DriverRide.Create(createCommand);
        return driverRide;
    }

    [Fact]
    public void Start_ShouldRaiseDriverRideStartedEvent_WhenStateIsValid()
    {
        // Arrange
        var driverRide = DriverRide;

        // Act
        driverRide.Start();

        // Assert
        ((IAggregateRoot)driverRide).GetDomainEvents().Should().ContainItemsAssignableTo<DriverRideStartedEvent>();
    }

    [Fact]
    public void Start_ShouldChangeRideStatusToStarted_WhenStateIsValid()
    {
        // Arrange
        var driverRide = DriverRide;
        // Act
        driverRide.Start();

        // Assert
        driverRide.Status.Should().Be(RideStatus.Started);
    }

    [Fact]
    public void Start_ShouldSetStartedAtToUtcNow_WhenStateIsValid()
    {
        // Arrange
        var driverRide = DriverRide;
        // Act
        driverRide.Start();

        // Assert
        driverRide.StartedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }

    [Fact]
    public void Start_ShouldThrowStartDriverRideException_WhenDriverRideIdIsDefault()
    {
        // Arrange
        var driverRideId = default(DriverRideId);
        var driverId = new DriverId(Guid.NewGuid());
        var driverIdentityId = new IdentityId(driverId.ToString());

        var driver = Driver.Create(driverId, driverIdentityId);
        var routeId = Guid.NewGuid();
        var vehicleId = new VehicleId(Guid.NewGuid());
        var createCommand = new CreateDriverRideCommand(driverRideId!, driver, routeId, vehicleId);
        // Act
        Action act = () => DriverRide.Create(createCommand);
        // Assert
        act.Should().Throw<StartDriverRideException>();
    }
}