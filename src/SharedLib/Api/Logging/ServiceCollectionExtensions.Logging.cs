using Ark.SharedLib.Api.Configurations;
using Serilog;

namespace Ark.SharedLib.Api.Logging;

public static class ServiceCollectionExtensions
{
    public static ConfigureHostBuilder ConfigureLogger(this ConfigureHostBuilder builder)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var basePath = Directory.GetCurrentDirectory();
        var configuration = AppConfigurations.Get(basePath, environment);

        Log.Logger = new LoggerConfiguration()
                     .ReadFrom.Configuration(configuration)
                     // .EnrichWithEventType()
                     // .Enrich.WithProperty("Version", ReflectionUtils.GetAssemblyVersion<Program>());
                     .CreateBootstrapLogger();

        builder.UseSerilog((context, services, loggerConfiguration) =>
            loggerConfiguration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services));

        return builder;
    }
}