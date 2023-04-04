using Ark.IdentityServer.Application.UCaller.Features.Commands.CheckHealth;
using Ark.IdentityServer.Application.UCaller.Features.Commands.CheckPhone;
using Ark.IdentityServer.Application.UCaller.Features.Commands.GetInfo;
using Ark.IdentityServer.Application.UCaller.Features.Commands.InitCall;
using Ark.IdentityServer.Application.UCaller.Features.Commands.InitRepeat;
using Ark.SharedLib.Common.Results;

namespace Ark.IdentityServer.Application.Contracts;

public interface IPhoneAuthService
{
    Task<Result<InitCallResponse>> InitCall(InitCallCommand command, CancellationToken cancellationToken = default);
    Task<Result<InitRepeatResponse>> InitResponse(int uid, CancellationToken cancellationToken = default);
    Task<Result<GetInfoResponse>> GetInfo(int uid, CancellationToken cancellationToken = default);
    Task<Result<CheckPhoneResponse>> CheckPhone(string phone, CancellationToken cancellationToken = default);
    Task<Result<CheckHealthResponse>> CheckHealth(CancellationToken cancellationToken = default);
}