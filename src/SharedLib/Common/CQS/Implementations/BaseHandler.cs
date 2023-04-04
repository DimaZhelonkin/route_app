using Ark.SharedLib.Common.CQS.Interfaces;
using Ark.SharedLib.Common.Results;
using MediatR;

namespace Ark.SharedLib.Common.CQS.Implementations;

public abstract class BaseHandler<TRequest, TResponse> : IHandler<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    where TResponse : Result
{
    #region IHandler<TRequest,TResponse> Members

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);

    #endregion
}