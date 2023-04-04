using Ark.SharedLib.Common.CQS.Interfaces;
using Ark.SharedLib.Common.Results;

namespace Ark.SharedLib.Common.CQS.Implementations;

public abstract class Query<TResponse> : IQuery<TResponse> where TResponse : Result
{
}

public abstract class QueryResult<TData> : Query<Result<TData>> where TData : class
{
}

// public abstract class PagedQuery<TData> : Query<PagedResult<TData>> where TData : class
// {
// }