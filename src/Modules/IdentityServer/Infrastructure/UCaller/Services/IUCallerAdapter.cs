using Ark.IdentityServer.Infrastructure.UCaller.Models.Responses;
using Ark.SharedLib.Common.Results;

namespace Ark.IdentityServer.Infrastructure.UCaller.Services;

public interface IUCallerAdapter
{
    Task<Result<InitCallResponse>> InitCall(ulong phoneNumber, CancellationToken cancellationToken = default);
    Task<Result<InitRepeatResponse>> InitRepeat(int uid, CancellationToken cancellationToken = default);

    Task<Result<GetInfoResponse>> GetInfo(int uid, CancellationToken cancellationToken = default);
    Task<Result<CheckPhoneResponse>> CheckPhone(string phone, CancellationToken cancellationToken = default);
    Task<Result<CheckHealthResponse>> CheckHealth(CancellationToken cancellationToken = default);
}