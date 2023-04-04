using Ardalis.Specification;
using Ark.Rides.Application.Features.Passenger.Commands.SendMatch;
using Ark.Rides.Application.Repositories;
using Ark.Rides.Application.Services;
using Ark.Rides.Domain.Aggregates.DriverRide;
using Ark.Rides.UnitTests.Domain.DriverRideTests;
using Ark.Routing.Contracts;
using Ark.Routing.Repositories;
using Ark.SharedLib.Application.Abstractions.Repositories;
using Bogus.DataSets;
using FluentAssertions;
using Moq;
using NetTopologySuite.Geometries;

namespace Ark.Rides.UnitTests.Application;

public class SendMatchCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldSendMatch_WhenStateIsValid()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var route = TestRoutes.Simple();
        var driverRide = TestDriverRides.Simple();
        var passenger = TestPassengers.Simple();
        
        var addressDataset = new Address();
        var startPoint = new Point(addressDataset.Latitude(), addressDataset.Longitude());
        var destination = new Point(addressDataset.Latitude(), addressDataset.Longitude());
        var unitOfWorkMocked = Mock.Of<IUnitOfWork>(x => x.SaveChangesAsync(cancellationToken) == Task.FromResult(1));
       
        var currentPassengerServiceStub = new Mock<ICurrentPassengerService>();
        currentPassengerServiceStub.Setup(x => x.GetPassenger(It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(passenger);
        var passengerRidesRepositoryStub = new Mock<IPassengerRidesRepository>();
        passengerRidesRepositoryStub.Setup(x => x.UnitOfWork)
                                    .Returns(unitOfWorkMocked);
        var driverRidesRepositoryStub = new Mock<IDriverRidesRepository>();
        driverRidesRepositoryStub.Setup(x => x.UnitOfWork)
                                 .Returns(unitOfWorkMocked);
        driverRidesRepositoryStub.Setup(x => x.FirstOrDefaultAsync(It.IsAny<ISpecification<DriverRide>>(),It.IsAny<CancellationToken>()))
                                 .ReturnsAsync(driverRide);
        var rideRequestsRepositoryStub = new Mock<IRideRequestsRepository>();
        rideRequestsRepositoryStub.Setup(x => x.UnitOfWork)
                                  .Returns(unitOfWorkMocked);
        var routeRepositoryStub = new Mock<IRouteRepository>();
        routeRepositoryStub.Setup(x => x.UnitOfWork)
                           .Returns(unitOfWorkMocked);
        var routingProviderStub = new Mock<IRoutingProvider>();
        routingProviderStub.Setup(x => x.GetRouteAsync(startPoint, destination, It.IsAny<CancellationToken>()))
                           .ReturnsAsync(route);
        var handler = new SendMatchCommandHandler(
            currentPassengerServiceStub.Object,
            passengerRidesRepositoryStub.Object,
            driverRidesRepositoryStub.Object,
            rideRequestsRepositoryStub.Object,
            routeRepositoryStub.Object,
            routingProviderStub.Object
        );
        var command = new SendMatchCommand(driverRide.Id, startPoint, destination);
        // Act
        var result = await handler.Handle(command, cancellationToken);
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}