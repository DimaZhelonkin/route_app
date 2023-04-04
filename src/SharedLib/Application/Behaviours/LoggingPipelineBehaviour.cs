using Ark.SharedLib.Application.Extensions;
using Ark.SharedLib.Common.Results;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ark.SharedLib.Application.Behaviours;

public class LoggingPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> _logger;

    public LoggingPipelineBehaviour(ILogger<LoggingPipelineBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    #region IPipelineBehavior<TRequest,TResponse> Members

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        //Request
        var startTime = DateTimeOffset.UtcNow;
        _logger.LogInformation(
            "Start request {@RequestName}, {@StartTime}",
            requestName, startTime);

        // var myType = request.GetType();
        // var props = new List<PropertyInfo>(myType.GetProperties());
        // foreach (var prop in props)
        // {
        //     var propValue = prop.GetValue(request, null);
        //     _logger.LogInformation("{Property} : {@Value}", prop.Name, propValue);
        // }
        var response = await next();

        var completionTime = DateTimeOffset.UtcNow;
        var executionTime = completionTime - startTime;

        if (response.IsFailure)
            _logger.LogError(
                "Request failure {@RequestName}, {@Errors} {@CompletionTime}, ExecutionTime: {@ExecutionTime}",
                requestName, completionTime, response.Errors, executionTime.ToReadableString());

        //Response
        _logger.LogInformation(
            "Completed request {@RequestName}, {@CompletionTime}, ExecutionTime: {@ExecutionTime}",
            requestName, completionTime, executionTime.ToReadableString());
        return response;
    }

    #endregion
}