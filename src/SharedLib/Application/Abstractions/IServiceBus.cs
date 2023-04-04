using MediatR;

namespace Ark.SharedLib.Application.Abstractions;

public interface IServiceBus
{
    Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        where TResponse : class;
}