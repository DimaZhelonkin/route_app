using Ark.Rides.Application.Repositories;
using Ark.Rides.Application.Services;
using Ark.Rides.Domain.Aggregates.DriverRide;
using Ark.Routing.Contracts;
using Ark.SharedLib.Application.Abstractions.Repositories;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Exceptions;
using Ark.SharedLib.Common.Results;
using Ark.Vehicles.Contracts;

namespace Ark.Rides.Application.Features.Driver.Commands.CreateDriverRide;

public record TestCommand() : CommandResult;
public class TestHandler : CommandHandler<TestCommand>
{
    public TestHandler()
    {
        
    }
    public override Task<Result> Handle(TestCommand command, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
public class CreateDriverRideCommandHandler : CommandHandler<CreateDriverRideCommand>
{
    private readonly ICurrentDriverService _currentDriverService;
    private readonly IDriverRidesRepository _driverRidesRepository;
    private readonly IRoutingProvider _routingProvider;
    private readonly IVehiclesProviderService _vehiclesProviderService;

    public CreateDriverRideCommandHandler(
        ICurrentDriverService currentDriverService,
        IDriverRidesRepository driverRidesRepository,
        IVehiclesProviderService vehiclesProviderService,
        IRoutingProvider routingProvider)
    {
        _driverRidesRepository = driverRidesRepository;
        _vehiclesProviderService = vehiclesProviderService;
        _routingProvider = routingProvider;
        _currentDriverService = currentDriverService;
    }

    public override async Task<Result> Handle(CreateDriverRideCommand request,
        CancellationToken cancellationToken = default)
    {
        var driver = await _currentDriverService.GetDriver(cancellationToken);
        if (driver is null)
            throw new ValidationException("Driver not found"); // TODO to custom exception
        var vehicle = await _vehiclesProviderService.GetByIdOrDefault(
            request.VehicleId, driver.IdentityId, cancellationToken);
        if (vehicle is null)
            throw new ValidationException("Driver hasn't attached any vehicles"); // TODO to custom exception
        var routeResult = await _routingProvider.GetRouteAsync(
            request.StartPoint, request.Destination, cancellationToken);
        if (routeResult.IsFailure)
            return routeResult;
        var route = routeResult.Value!;
        var driverRideId = new DriverRideId(Guid.NewGuid());
        var createCommand = new Domain.Aggregates.DriverRide.Commands.CreateDriverRideCommand(
            driverRideId, driver, route.Id, vehicle.Id);
        var ride = DriverRide.Create(createCommand);
        await _driverRidesRepository.AddAsync(ride, cancellationToken);
        await _driverRidesRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}