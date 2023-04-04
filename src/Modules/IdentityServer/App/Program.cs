using Ark.SharedLib.Api.Logging;
using Ark.SharedLib.Persistence;
using Arl.IdentityServer.Api;
using Keycloak.AuthServices.Authentication;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
var services = builder.Services;
var configuration = builder.Configuration;
var host = builder.Host;

host.ConfigureLogger();
host.ConfigureKeycloakConfigurationSource(); // TODO NOT WORKING
try
{
    var app = builder
              .ConfigureServices()
              .ConfigurePipeline();

    // this seeding is only for the template to bootstrap the DB and users.
    // in production you will likely want a different approach.
    using var scope = app.Services.CreateScope();
    var serviceProvider = scope.ServiceProvider;
    await DatabaseInitializer.DatabaseInitialisingAsync(args, serviceProvider);
    await app.RunAsync();
    Log.Information("Stopped cleanly");
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occured during bootstrapping");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}