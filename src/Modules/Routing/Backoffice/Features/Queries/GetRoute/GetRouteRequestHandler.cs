using Ark.SharedLib.Common.CQS.Implementations;
using Ark.SharedLib.Common.Results;
using Ark.SharedLib.Common.Results.Extensions;
using AutoMapper;
using MediatR;

namespace Ark.Routing.Features.Queries.GetRoute;

/// <summary>
/// </summary>
public class GetRouteRequestHandler : QueryHandler<GetRouteRequest, GetRouteResponse>
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public GetRouteRequestHandler(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    public override async Task<Result<GetRouteResponse>> Handle(GetRouteRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<GetRouteQuery.GetRouteQuery>(request);
        var result = await _sender.Send(command, cancellationToken);
        var response = _mapper.Map<Result<GetRouteResponse>>(result);
        return response;
    }
}