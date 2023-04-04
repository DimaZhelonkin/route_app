using Ark.Rides.Application.Repositories;
using Ark.Rides.Application.Services;
using Ark.Rides.Application.Specifications.DriverRideSpecs;
using Ark.Rides.Domain.Aggregates.PassengerRide;
using Ark.Rides.Domain.Aggregates.RideRequest;
using Ark.Routing.Contracts;
using Ark.Routing.Repositories;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using FluentValidation;

namespace Ark.Rides.Application.Features.Passenger.Commands.SendMatch;

public class SendMatchCommandHandler : CommandHandler<SendMatchCommand>
{
    private readonly ICurrentPassengerService _currentPassengerService;
    private readonly IDriverRidesRepository _driverRidesRepository;
    private readonly IPassengerRidesRepository _passengerRidesRepository;
    private readonly IRideRequestsRepository _rideRequestsRepository;
    private readonly IRouteRepository _routeRepository;
    private readonly IRoutingProvider _routingProvider;

    public SendMatchCommandHandler(
        ICurrentPassengerService currentPassengerService,
        IPassengerRidesRepository passengerRidesRepository,
        IDriverRidesRepository driverRidesRepository,
        IRideRequestsRepository rideRequestsRepository,
        IRouteRepository routeRepository,
        IRoutingProvider routingProvider)
    {
        _currentPassengerService = currentPassengerService;
        _driverRidesRepository = driverRidesRepository;
        _rideRequestsRepository = rideRequestsRepository;
        _routeRepository = routeRepository;
        _passengerRidesRepository = passengerRidesRepository;
        _routingProvider = routingProvider;
    }

    public override async Task<Result> Handle(SendMatchCommand command, CancellationToken cancellationToken = default)
    {
        // TODO make it as transaction
        var passenger = await _currentPassengerService.GetPassenger(cancellationToken);
        if (passenger is null)
            throw new ValidationException("Passenger not found");
        // TODO когда посылается этот запрос, по идее уже было построено на карте визуальное пересечение
        // TODO соответственно нужно его уже сохранять и потом здесь находить, для оптимизации запросов к routing api
        // TODO можно в кэш загонять по точкам, по которым ищется пересечение, и здесь из кэша по входным параметрам находить

        var route = await _routingProvider.GetRouteAsync(
            command.StartPoint, command.Destination, cancellationToken);
        await _routeRepository.AddAsync(route, cancellationToken);
        await _routeRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        
        var passengerRideId = new PassengerRideId(Guid.NewGuid());
        var passengerRide = PassengerRide.Create(passengerRideId, passenger, route.Value!.Id);
        await _passengerRidesRepository.AddAsync(passengerRide, cancellationToken);
        await _passengerRidesRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        var getDriverRideByIdSpec = new GetDriverRideByIdSpec(command.DriverRideId);
        var driverRide = await _driverRidesRepository.FirstOrDefaultAsync(getDriverRideByIdSpec, cancellationToken);
        if (driverRide is null)
            throw new ValidationException("Driver ride not found");

        var rideRequestId = new RideRequestId(Guid.NewGuid());
        var rideRequest = RideRequest.Create(rideRequestId, driverRide, passengerRide);
        await _rideRequestsRepository.AddAsync(rideRequest, cancellationToken);
        await _rideRequestsRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}