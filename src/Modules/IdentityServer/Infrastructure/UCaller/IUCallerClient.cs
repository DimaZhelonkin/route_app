using Ark.IdentityServer.Infrastructure.UCaller.Models.Requests;
using Refit;

namespace Ark.IdentityServer.Infrastructure.UCaller;

[Headers("Content-Type: application/json")]
public interface IUCallerClient
{
    [Post("/initCall")]
    Task<HttpResponseMessage> InitCall([Body] InitCallRequest content, CancellationToken cancellationToken = default);

    [Post("/initRepeat")]
    Task<HttpResponseMessage> InitRepeat([Body] InitRepeatRequest content,
        CancellationToken cancellationToken = default);

    [Get("/getInfo")]
    Task<HttpResponseMessage> GetInfo([Body] GetInfoRequest content, CancellationToken cancellationToken = default);

    [Get("/checkPhone")]
    Task<HttpResponseMessage> CheckPhone([Body] CheckPhoneRequest content,
        CancellationToken cancellationToken = default);

    [Get("/health")]
    Task<HttpResponseMessage> Health(CancellationToken cancellationToken = default);
}