using System.Reflection;
using Ark.Infrastructure.Shared.Filters;
using Keycloak.AuthServices.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Ark.SharedLib.Api.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        KeycloakAuthenticationOptions keycloakOptions = new();

        configuration
            .GetSection(KeycloakAuthenticationOptions.Section)
            .Bind(keycloakOptions, opt => opt.BindNonPublicProperties = true);

        services.Configure<KeycloakAuthenticationOptions>(x => configuration
                                                               .GetSection(KeycloakAuthenticationOptions.Section)
                                                               .Bind(x, opt => opt.BindNonPublicProperties = true));


        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();
            options.OperationFilter<TagByAreaNameOperationFilter>();
            options.OperationFilter<AuthResponsesOperationFilter>();
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Ark API",
                Description = "Ark API Description",
                TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Ark",
                    Email = string.Empty,
                    // TODO change it
                    Url = new Uri("https://example.com/contact"),
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://example.com/license"),
                },
            });
            options.ResolveConflictingActions(a => a.First());

            // options.DocInclusionPredicate((docName, description) => true);

            // Define the Bearer auth scheme that's in use
            var jwtSecurityScheme = new OpenApiSecurityScheme
            {
                Name = "JWT Authentication",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer", // must be lower case
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme,
                },
            };
            options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
            var openApiSecurityScheme = new OpenApiSecurityScheme
            {
                Name = "Auth",
                Type = SecuritySchemeType.OAuth2,
                Reference = new OpenApiReference
                {
                    Id = "OAuth",
                    Type = ReferenceType.SecurityScheme,
                },
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{keycloakOptions.KeycloakUrlRealm}/protocol/openid-connect/auth"),
                        TokenUrl = new Uri($"{keycloakOptions.KeycloakUrlRealm}/protocol/openid-connect/token"),
                        Scopes = new Dictionary<string, string>(),
                    },
                },
            };
            // options.AddSecurityDefinition(openApiSecurityScheme.Reference.Id, openApiSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {jwtSecurityScheme, Array.Empty<string>()},
                // {openApiSecurityScheme, Array.Empty<string>()},
            });

            // var authority = configuration["Authentication:JwtBearer:Authority"];
            // // Scheme Definition 
            // options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            // {
            //     Type = SecuritySchemeType.OAuth2,
            //
            //     Flows = new OpenApiOAuthFlows
            //     {
            //         AuthorizationCode = new OpenApiOAuthFlow
            //         {
            //             AuthorizationUrl = new Uri(authority + "/connect/authorize"),
            //             TokenUrl = new Uri(authority + "/connect/token"),
            //             Scopes =
            //             {
            //                 {"astrum.api", "ASTRUM API - full access"}
            //             }
            //         }
            //     }
            // });
            // // Apply Scheme globally
            // options.AddSecurityRequirement(new OpenApiSecurityRequirement
            // {
            //     {
            //         new OpenApiSecurityScheme
            //         {
            //             Reference = new OpenApiReference
            //                 {Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme}
            //         },
            //         new[] {"astrum.api"}
            //     }
            // });


            //add summaries to swagger
            var canShowSummaries = configuration.GetValue<bool>("Swagger:ShowSummaries");
            if (canShowSummaries)
            {
                var assemblyLocation = Assembly.GetExecutingAssembly().Location;
                var assemblyFolder = Path.GetDirectoryName(assemblyLocation);
                var xmlDocFiles = Directory.GetFiles(assemblyFolder, "*.Backoffice.xml");
                foreach (var xmlDocFile in xmlDocFiles)
                    options.IncludeXmlComments(xmlDocFile);
            }
        });
        services.AddSwaggerGenNewtonsoftSupport();
        return services;
    }

    public static IApplicationBuilder UseApplicationSwagger(this IApplicationBuilder app, IConfiguration configuration)
    {
        KeycloakAuthenticationOptions keycloakAuthenticationOptions = new();

        configuration
            .GetSection(KeycloakAuthenticationOptions.Section)
            .Bind(keycloakAuthenticationOptions, opt => opt.BindNonPublicProperties = true);


        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("v1/swagger.json", "Ark API V1");
            options.DisplayRequestDuration();
            options.OAuthUsePkce();
            options.OAuthUseBasicAuthenticationWithAccessCodeGrant();
            options.OAuthScopes("ark.api");
            options.OAuthAppName("Ark API V1");
            options.OAuthClientId(keycloakAuthenticationOptions.Resource);
            options.OAuthClientSecret(null);
            options.OAuthUsername("ArkUsername");
            options.ShowExtensions();
            options.EnableFilter();
            options.EnableValidator();
            options.EnableDeepLinking();
            options.EnablePersistAuthorization();
            options.EnableTryItOutByDefault();
        });

        return app;
    }
}