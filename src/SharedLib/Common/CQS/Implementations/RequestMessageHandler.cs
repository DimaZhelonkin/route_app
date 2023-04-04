using Ark.SharedLib.Common.Results;
using MediatR;

namespace Ark.SharedLib.Common.CQS.Implementations;

public abstract class RequestMessageHandler<TRequest, TResponse> : MessageBaseHandler<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : Result

{
}