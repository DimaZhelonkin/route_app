using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Domain.Aggregates.Driver;
using Ark.Rides.Domain.Aggregates.DriverRide;
using Ark.Rides.Domain.Aggregates.DriverRide.Commands;
using Ark.Rides.Domain.Aggregates.DriverRide.Events;
using Ark.Rides.Domain.Enums;
using Ark.SharedLib.Domain.Interfaces;
using Ark.Vehicles.Aggregates;
using FluentAssertions;

namespace Ark.Rides.UnitTests.Domain.DriverRideTests;

public class FinishDriverRideTests
{
    public FinishDriverRideTests()
    {
        DriverRide = CreateFixtureDriverRide();
    }

    public DriverRide DriverRide { get; init; }

    private static DriverRide CreateFixtureDriverRide()
    {
        var driverRideId = new DriverRideId(Guid.NewGuid());
        var driverId = new DriverId(Guid.NewGuid());        var driverIdentityId = new IdentityId(driverId.ToString());

        var driver = Driver.Create(driverId, driverIdentityId);
        var routeId = Guid.NewGuid();
        var vehicleId = new VehicleId(Guid.NewGuid());
        var createCommand = new CreateDriverRideCommand(driverRideId, driver, routeId, vehicleId);
        var driverRide = DriverRide.Create(createCommand);
        driverRide.Start();
        return driverRide;
    }

    [Fact]
    public void Finish_ShouldRaiseDriverRideFinishedEvent_WhenDriverRideCouldBeFinished()
    {
        // Arrange
        var driverRide = DriverRide;
        // Act
        driverRide.Finish();

        // Assert
        ((IAggregateRoot)driverRide).GetDomainEvents().Should().ContainItemsAssignableTo<DriverRideFinishedEvent>();
    }
    
    [Fact]
    public void Start_ShouldChangeRideStatusToFinished_WhenStateIsValid()
    {
        // Arrange
        var driverRide = DriverRide;
        // Act
        driverRide.Finish();

        // Assert
        driverRide.Status.Should().Be(RideStatus.Finished);
    }
    
    [Fact]
    public void Start_ShouldSetFinishedAtToUtcNow_WhenStateIsValid()
    {
        // Arrange
        var driverRide = DriverRide;
        // Act
        driverRide.Finish();

        // Assert
        driverRide.FinishedAt.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
    }
}