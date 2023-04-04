using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Ark.Infrastructure.Extensions;

public static class ApiVersioningServiceExtension
{
    public static IServiceCollection AddApiVersioningExtension(this IServiceCollection services) =>
        services.AddApiVersioning(config =>
        {
            // use API versioning with headers
            config.ApiVersionReader = new HeaderApiVersionReader("api-version");
            // Default API Version
            config.DefaultApiVersion = new ApiVersion(1, 0);
            // use default version when version is not specified
            config.AssumeDefaultVersionWhenUnspecified = true;
            // Advertise the API versions supported for the particular endpoint
            config.ReportApiVersions = true;
        });
}