using Ark.SharedLib.Common.CQS.Interfaces;
using Ark.SharedLib.Common.Results;

namespace Ark.SharedLib.Common.CQS.Implementations;

public abstract record Command<TResponse> : ICommand<TResponse> where TResponse : Result
{
}

public abstract record CommandResult : Command<Result>
{
    
}

public abstract record CommandResult<TData> : Command<Result<TData>> where TData : class
{
}

// public abstract class PagedCommand<TData> : Command<PagedResult<TData>> where TData : class
// {
//     
// }