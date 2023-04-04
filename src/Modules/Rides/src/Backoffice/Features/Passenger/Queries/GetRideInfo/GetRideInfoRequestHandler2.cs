using Ark.Rides.Application.Features.Passenger.Queries.GetRideInfo;
using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using AutoMapper;
using MediatR;

namespace Ark.Rides.Backoffice.Features.Passenger.Queries.GetRideInfo;

/// <summary>
/// </summary>
public class GetRideInfoRequestHandler : QueryHandler<GetRideInfoRequest, GetRideInfoResponseData>
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public GetRideInfoRequestHandler(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    public override async Task<Result<GetRideInfoResponseData>> Handle(GetRideInfoRequest query,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<GetRideInfoQuery>(query);
        var result = await _mediator.Send(command, cancellationToken);
        return result;
    }
}