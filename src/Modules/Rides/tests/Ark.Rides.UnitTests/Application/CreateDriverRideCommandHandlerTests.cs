using Ark.Rides.Application.Contracts;
using Ark.Rides.Application.Features.Driver.Commands.CreateDriverRide;
using Ark.Rides.Application.Providers;
using Ark.Rides.Application.Repositories;
using Ark.Rides.Application.Services;
using Ark.Rides.Domain.Aggregates.Driver;
using Ark.Rides.UnitTests.Domain.DriverRideTests;
using Ark.Routing.Contracts;
using Ark.SharedLib.Application.Abstractions.Repositories;
using Ark.Vehicles.Aggregates;
using Ark.Vehicles.Contracts;
using FluentAssertions;
using Moq;

namespace Ark.Rides.UnitTests.Application;

public class CreateDriverRideCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCreateDriverRide_WhenStateIsValid()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var driver = TestDrivers.Simple();
        var vehicleOwnerId = new VehicleOwnerId(driver.Id.Value);
        var vehicleOwner = TestVehicleOwners.Simple(vehicleOwnerId, driver.IdentityId);
        var vehicle = TestVehicles.Simple(vehicleOwner);
        var route = TestRoutes.Simple();
        var driverRide = TestDriverRides.Simple();

        var currentDriverServiceStub = new Mock<ICurrentDriverService>();
        currentDriverServiceStub.Setup(p => p.GetDriver(cancellationToken))
                                .ReturnsAsync(driver);
        var driversProviderStub = new Mock<IDriversProvider>();
        driversProviderStub.Setup(p => p.GetDriver(driver.IdentityId, cancellationToken))
                           .ReturnsAsync(driver);

        var driverVehiclesServiceStub = new Mock<IDriverVehiclesService>();
        driverVehiclesServiceStub.Setup(s => s.GetDefaultVehicle(It.IsAny<DriverId>(), It.IsAny<CancellationToken>()))
                                 .ReturnsAsync(vehicle);

        var vehiclesProviderServiceStub = new Mock<IVehiclesProviderService>();
        vehiclesProviderServiceStub
            .Setup(s => s.GetById(It.IsAny<VehicleId>(), It.IsAny<VehicleOwnerId>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(vehicle);

        var routingProviderStub = new Mock<IRoutingProvider>();
        routingProviderStub.Setup(p => p.GetRouteAsync(route.StartPoint, route.DestinationPoint, cancellationToken))
                           .ReturnsAsync(route);

        var driverRidesRepositoryStub = new Mock<IDriverRidesRepository>();
        driverRidesRepositoryStub.Setup(x => x.AddAsync(driverRide, cancellationToken))
                                 .ReturnsAsync(driverRide);

        var unitOfWorkMocked = Mock.Of<IUnitOfWork>(x => x.SaveChangesAsync(cancellationToken) == Task.FromResult(1));
        driverRidesRepositoryStub.Setup(x => x.UnitOfWork)
                                 .Returns(unitOfWorkMocked);

        var handler = new CreateDriverRideCommandHandler(currentDriverServiceStub.Object,
            driverRidesRepositoryStub.Object,
            vehiclesProviderServiceStub.Object,
            routingProviderStub.Object
        );

        const uint stubCapacity = 3;
        const decimal stubMinPrice = 10;
        var command = new CreateDriverRideCommand(
            route.StartPoint,
            route.DestinationPoint,
            stubCapacity,
            stubMinPrice,
            vehicle.Id
        );
        // Act
        var result = await handler.Handle(command, cancellationToken);
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}