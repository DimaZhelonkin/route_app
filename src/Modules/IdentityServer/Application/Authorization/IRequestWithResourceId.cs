using MediatR;

namespace Ark.IdentityServer.Application.Authorization;

public interface IRequestWithResourceId : IRequest
{
    string ResourceId { get; }
}