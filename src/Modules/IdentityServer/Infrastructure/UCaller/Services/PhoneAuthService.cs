using Ark.IdentityServer.Application.Contracts;
using Ark.IdentityServer.Application.UCaller.Features.Commands.CheckHealth;
using Ark.IdentityServer.Application.UCaller.Features.Commands.InitCall;
using Ark.SharedLib.Common.Results;
using Ark.SharedLib.Common.Results.Extensions;
using AutoMapper;
using ApplicationInitCallResponse = Ark.IdentityServer.Application.UCaller.Features.Commands.InitCall.InitCallResponse;
using InfrastructureInitCallResponse = Ark.IdentityServer.Infrastructure.UCaller.Models.Responses.InitCallResponse;
using ApplicationInitRepeatResponse =
    Ark.IdentityServer.Application.UCaller.Features.Commands.InitRepeat.InitRepeatResponse;
using InfrastructureInitRepeatResponse = Ark.IdentityServer.Infrastructure.UCaller.Models.Responses.InitRepeatResponse;
using ApplicationCheckPhoneResponse =
    Ark.IdentityServer.Application.UCaller.Features.Commands.CheckPhone.CheckPhoneResponse;
using InfrastructureCheckPhoneResponse = Ark.IdentityServer.Infrastructure.UCaller.Models.Responses.CheckPhoneResponse;
using ApplicationGetInfoResponse = Ark.IdentityServer.Application.UCaller.Features.Commands.GetInfo.GetInfoResponse;
using InfrastructureGetInfoResponse = Ark.IdentityServer.Infrastructure.UCaller.Models.Responses.GetInfoResponse;

namespace Ark.IdentityServer.Infrastructure.UCaller.Services;

public class PhoneAuthService : IPhoneAuthService
{
    private readonly IMapper _mapper;
    private readonly IUCallerAdapter _uCallerAdapter;

    public PhoneAuthService(IUCallerAdapter uCallerAdapter, IMapper mapper)
    {
        _uCallerAdapter = uCallerAdapter;
        _mapper = mapper;
    }

    #region IPhoneAuthService Members

    public async Task<Result<ApplicationInitCallResponse>> InitCall(InitCallCommand command,
        CancellationToken cancellationToken = default)
    {
        var result = await _uCallerAdapter.InitCall(command.Phone, cancellationToken);
        return result.Map<InfrastructureInitCallResponse, ApplicationInitCallResponse>();
    }

    public async Task<Result<ApplicationInitRepeatResponse>> InitResponse(int uid,
        CancellationToken cancellationToken = default)
    {
        var result = await _uCallerAdapter.InitRepeat(uid, cancellationToken);
        return _mapper.Map<ApplicationInitRepeatResponse>(result);
    }

    public async Task<Result<ApplicationGetInfoResponse>> GetInfo(int uid,
        CancellationToken cancellationToken = default)
    {
        var result = await _uCallerAdapter.GetInfo(uid, cancellationToken);
        return _mapper.Map<ApplicationGetInfoResponse>(result);
    }

    public async Task<Result<ApplicationCheckPhoneResponse>> CheckPhone(string phone,
        CancellationToken cancellationToken = default)
    {
        var result = await _uCallerAdapter.CheckPhone(phone, cancellationToken);
        return _mapper.Map<ApplicationCheckPhoneResponse>(result);
    }

    public async Task<Result<CheckHealthResponse>> CheckHealth(CancellationToken cancellationToken = default)
    {
        var result = await _uCallerAdapter.CheckHealth(cancellationToken);
        return _mapper.Map<CheckHealthResponse>(result);
    }

    #endregion
}