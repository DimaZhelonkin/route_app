﻿namespace Ark.IdentityServer.Infrastructure.UCaller;

public class LoggingHandler : DelegatingHandler
{
    public LoggingHandler(HttpMessageHandler innerHandler)
        : base(innerHandler)
    {
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        Console.WriteLine("Request:");
        Console.WriteLine(request.ToString());
        if (request.Content != null) Console.WriteLine(await request.Content.ReadAsStringAsync());
        Console.WriteLine();

        var response = await base.SendAsync(request, cancellationToken);

        Console.WriteLine("Response:");
        Console.WriteLine(response.ToString());
        if (response.Content != null) Console.WriteLine(await response.Content.ReadAsStringAsync());
        Console.WriteLine();

        return response;
    }
}