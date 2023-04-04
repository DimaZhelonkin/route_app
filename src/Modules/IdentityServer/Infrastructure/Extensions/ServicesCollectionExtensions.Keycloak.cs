using System.Text.Json;
using System.Text.Json.Serialization;
using Ark.IdentityServer.Infrastructure.Keycloak;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Keycloak.AuthServices.Sdk.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Ark.IdentityServer.Infrastructure.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomizedKeycloak(this IServiceCollection services, IConfiguration configuration)
    {
        // based on token forwarding HttpClient middleware
        // services.AddKeycloakProtectionHttpClient(configuration);
        services.AddCustomAuthentication(configuration);
        // requires confidential client
        services.AddKeycloakAdminHttpClient(configuration);

        services.AddAuthKeycloakClient(configuration);
        services.AddTransient<IKeyCloakAuthenticationService, KeyCloakAuthenticationService>();
        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();
        services.Configure<KeycloakAuthenticationOptions>(x => configuration
                                                               .GetSection(KeycloakAuthenticationOptions.Section)
                                                               .Bind(x, opt => opt.BindNonPublicProperties = true));
        services.AddKeycloakAuthentication(configuration, o =>
        {
            o.RequireHttpsMetadata = false;
        });

        services.AddAuthorization(o =>
        {
            // o.FallbackPolicy = new AuthorizationPolicyBuilder()
            // .RequireAuthenticatedUser()
            // .Build();

            o.AddPolicy("ProtectedResource", b =>
            {
                // b.AddRequirements(new DecisionRequirement("workspaces", "workspaces:read"));
                b.RequireProtectedResource("workspaces", "workspaces:read");
            });

            o.AddPolicy("RealmRole", b =>
            {
                // b..AddRequirements(new RealmAccessRequirement("SuperManager"));
                b.RequireRealmRoles("SuperManager");
            });

            o.AddPolicy("ClientRole", b =>
            {
                // b.AddRequirements(new ResourceAccessRequirement(default, "Manager"));
                b.RequireResourceRoles("Manager");
            });
            // o.AddPolicy("IsAdmin", b =>
            // {
            //     b.RequireRole("r-admin");
            // });
        }).AddKeycloakAuthorization(configuration);
        services.AddSingleton<IAuthorizationPolicyProvider, ProtectedResourcePolicyProvider>();
        //
        // services.AddHeimGuard<UserPolicyHandler>()
        //     .MapAuthorizationPolicies()
        //     .AutomaticallyCheckPermissions();
        return services;
    }


    public static IHttpClientBuilder AddAuthKeycloakClient(
        this IServiceCollection services,
        IConfiguration configuration,
        Action<HttpClient>? configureClient = default,
        string? keycloakClientSectionName = default)
    {
        KeycloakAdminClientOptions options = new();

        configuration
            .GetSection(keycloakClientSectionName ?? KeycloakAdminClientOptions.Section)
            .Bind(options);
        return services.AddAuthKeycloakClient(options, configureClient);
    }

    public static IHttpClientBuilder AddAuthKeycloakClient(this IServiceCollection services,
        KeycloakAdminClientOptions keycloakOptions,
        Action<HttpClient>? configureClient = default) =>
        services.AddRefitClient<IKeycloakAuthClient>(GetKeycloakClientRefitSettings())
                .ConfigureHttpClient(client =>
                {
                    var baseUrl = new Uri(keycloakOptions.AuthServerUrl.TrimEnd('/'));
                    client.BaseAddress = baseUrl;
                    configureClient?.Invoke(client);
                });

    private static RefitSettings GetKeycloakClientRefitSettings() =>
        new RefitSettings
        {
            ContentSerializer = new SystemTextJsonContentSerializer(
                new JsonSerializerOptions(JsonSerializerDefaults.Web)
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                }),
        };
}