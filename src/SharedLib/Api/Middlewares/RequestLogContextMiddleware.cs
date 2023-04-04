using Ark.Infrastructure.Extensions;
using Serilog.Context;

namespace Ark.SharedLib.Api.Middlewares;

public class RequestLogContextMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLogContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", context.GetCorrelationId()))
            return _next.Invoke(context);
    }
}