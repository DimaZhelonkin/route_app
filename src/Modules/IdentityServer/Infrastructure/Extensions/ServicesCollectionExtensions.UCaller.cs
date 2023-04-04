using System.Text.Json;
using System.Text.Json.Serialization;
using Ark.IdentityServer.Application.Contracts;
using Ark.IdentityServer.Infrastructure.UCaller;
using Ark.IdentityServer.Infrastructure.UCaller.Configs;
using Ark.IdentityServer.Infrastructure.UCaller.Services;
using IdentityModel.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Ark.IdentityServer.Infrastructure.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddUCallerServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddUCallerOptions(configuration);
        services.AddUCallerClient(configuration);

        services.AddTransient<IUCallerAdapter, UCallerAdapter>();
        services.AddTransient<IPhoneAuthService, PhoneAuthService>();

        return services;
    }

    private static IServiceCollection AddUCallerClient(this IServiceCollection services, IConfiguration configuration,
        Action<HttpClient>? configureClient = default)
    {
        UCallerOptions options = new();
        configuration
            .GetSection(UCallerOptions.Section)
            .Bind(options);

        var refitSettings = new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                }),
        };

        services.AddRefitClient<IUCallerClient>(refitSettings)
                .ConfigureHttpClient(client =>
                {
                    var baseUrl = new Uri(options.Url.TrimEnd('/'));
                    client.BaseAddress = baseUrl;
                    // client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", $"{options.SecretKey}.{options.ServiceId}");
                    client.SetBearerToken($"{options.SecretKey}.{options.ServiceId}");
                    configureClient?.Invoke(client);
                });


        // Register Logging for HttpClient
        services.AddHttpClientLogging(configuration);
        services.EnableLoggingForBadRequests();
        // services.AddHeaderPropagation(options => { options.HeaderNames.Add("X-My-Header"); });
        return services;
    }

    private static IServiceCollection AddUCallerOptions(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<UCallerOptions>(x => configuration.GetSection(UCallerOptions.Section).Bind(x));
}