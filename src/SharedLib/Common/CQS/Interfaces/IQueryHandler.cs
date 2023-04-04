using Ark.SharedLib.Common.Results;

namespace Ark.SharedLib.Common.CQS.Interfaces;

public interface IQueryHandler<in TQuery, TResponse> : IHandler<TQuery, Result<TResponse>>
    where TQuery : class, IQuery<Result<TResponse>>
    where TResponse : class
{
}