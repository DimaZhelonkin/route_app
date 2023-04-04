using Ark.SharedLib.Common.CQS.Interfaces;
using Ark.SharedLib.Common.Results;

namespace Ark.SharedLib.Common.CQS.Implementations;

public abstract class QueryHandler<TQuery, TData> : RequestMessageHandler<TQuery, Result<TData>>
    where TQuery : class, IQuery<Result<TData>>
    where TData : class
{
}

// public abstract class QueryPagedResultHandler<TQuery, TData> : QueryHandler<TQuery, PagedResult<TData>>
//     where TQuery : PagedQuery<TData>
//     where TData : class
// {
// }