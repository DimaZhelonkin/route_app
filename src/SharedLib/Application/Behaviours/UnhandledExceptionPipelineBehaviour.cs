using MediatR;
using Microsoft.Extensions.Logging;

namespace Ark.SharedLib.Application.Behaviours;

public class UnhandledExceptionPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<UnhandledExceptionPipelineBehaviour<TRequest, TResponse>> _logger;

    public UnhandledExceptionPipelineBehaviour(ILogger<UnhandledExceptionPipelineBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    #region IPipelineBehavior<TRequest,TResponse> Members

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogError(ex, "Application Request: Unhandled Exception for Request {Name} {@Request}", requestName,
                request);
            throw;
        }
    }

    #endregion
}