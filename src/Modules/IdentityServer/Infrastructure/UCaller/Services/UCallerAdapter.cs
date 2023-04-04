using Ark.IdentityServer.Infrastructure.UCaller.Models.Requests;
using Ark.IdentityServer.Infrastructure.UCaller.Models.Responses;
using Ark.SharedLib.Common.Results;
using Newtonsoft.Json;

namespace Ark.IdentityServer.Infrastructure.UCaller.Services;

internal class UCallerAdapter : IUCallerAdapter
{
    private readonly IUCallerClient _uCallerClient;

    public UCallerAdapter(IUCallerClient uCallerClient)
    {
        _uCallerClient = uCallerClient;
    }

    #region IUCallerAdapter Members

    public async Task<Result<InitRepeatResponse>> InitRepeat(int uid, CancellationToken cancellationToken = default)
    {
        var content = new InitRepeatRequest
        {
            Uid = uid,
        };
        var response = await _uCallerClient.InitRepeat(content, cancellationToken);
        var result = await ParseJson<InitRepeatResponse>(response, cancellationToken);
        return result;
    }

    public async Task<Result<GetInfoResponse>> GetInfo(int uid, CancellationToken cancellationToken = default)
    {
        var content = new GetInfoRequest
        {
            Uid = uid,
        };
        var response = await _uCallerClient.GetInfo(content, cancellationToken);
        var result = await ParseJson<GetInfoResponse>(response, cancellationToken);
        return result;
    }

    public async Task<Result<CheckPhoneResponse>> CheckPhone(string phone,
        CancellationToken cancellationToken = default)
    {
        var content = new CheckPhoneRequest
        {
            Phone = phone,
        };
        var response = await _uCallerClient.CheckPhone(content, cancellationToken);
        var result = await ParseJson<CheckPhoneResponse>(response, cancellationToken);
        return result;
    }

    public async Task<Result<CheckHealthResponse>> CheckHealth(CancellationToken cancellationToken = default)
    {
        var response = await _uCallerClient.Health(cancellationToken);
        var result = await ParseJson<CheckHealthResponse>(response, cancellationToken);
        return result;
    }

    public async Task<Result<InitCallResponse>> InitCall(ulong phoneNumber,
        CancellationToken cancellationToken = default)
    {
        var content = new InitCallRequest
        {
            Phone = phoneNumber,
        };
        var response = await _uCallerClient.InitCall(content, cancellationToken);
        var result = await ParseJson<InitCallResponse>(response, cancellationToken);
        return result;
    }

    #endregion

    private async Task<Result<TResponse>> ParseJson<TResponse>(HttpResponseMessage messageResponse,
        CancellationToken cancellationToken = default) where TResponse : BaseResponse
    {
        var dataAsString = await messageResponse.Content.ReadAsStringAsync(cancellationToken);
        var baseResponse = JsonConvert.DeserializeObject<BaseResponse>(dataAsString);
        if (baseResponse is null)
            return Result.Error<TResponse>("ParseJson",
                $"Unexpected Error: Couldn't deserialize response: \n\r {dataAsString}");
        if (baseResponse.Status)
        {
            var successResponse = JsonConvert.DeserializeObject<TResponse>(dataAsString);
            return Result.Success(successResponse);
        }

        var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(dataAsString);
        return Result.Error<TResponse>("ParseJson", errorResponse.Error);
    }
}