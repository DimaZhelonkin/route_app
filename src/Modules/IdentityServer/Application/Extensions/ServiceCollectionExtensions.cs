using System.Reflection;
using Ark.IdentityServer.Application.Authorization;
using Ark.IdentityServer.Application.Authorization.FromIdentity.Handlers;
using Ark.IdentityServer.Application.Contracts;
using Ark.IdentityServer.Application.Services;
using Ark.IdentityServer.DomainServices.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ark.IdentityServer.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        services.AddDomainServices();
        services.AddMediatR(x =>
            x.RegisterServicesFromAssembly(currentAssembly)
        );
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

        services.AddOptions();
        services.Configure<PasswordHashingOptions>(x =>
            configuration.GetSection(PasswordHashingOptions.Section).Bind(x));
        services.AddTransient<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }


    private static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services) =>
        // services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, UserOperationAuthorizationHandler>();
}