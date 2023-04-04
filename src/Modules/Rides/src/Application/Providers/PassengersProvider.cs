using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.Rides.Application.Repositories;
using Ark.Rides.Application.Specifications.PassengerSpecs;
using Ark.Rides.Domain.Aggregates.Passenger;

namespace Ark.Rides.Application.Providers;

public class PassengersProvider : IPassengersProvider
{
    private readonly IPassengersRepository _passengersRepository;

    public PassengersProvider(IPassengersRepository passengersRepository)
    {
        _passengersRepository = passengersRepository;
    }

    #region IPassengersProvider Members

    public async Task<Passenger?> GetPassengerByIdentityId(IdentityId identityId, CancellationToken cancellationToken)
    {
        var specification = new GetPassengerByIdentityIdSpec(identityId);
        var passenger = await _passengersRepository.FirstOrDefaultAsync(specification, cancellationToken);
        return passenger;
    }

    #endregion
}