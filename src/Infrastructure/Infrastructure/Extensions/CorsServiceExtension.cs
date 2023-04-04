namespace Ark.Infrastructure.Extensions;

public static class CorsServiceExtension
{
    // public static IServiceCollection AddCorsService(this IServiceCollection services, string policyName, IWebHostEnvironment env)
    // {
    //     if (env.IsDevelopment() || env.IsEnvironment(LocalConfig.IntegrationTestingEnvName) ||
    //         env.IsEnvironment(LocalConfig.FunctionalTestingEnvName))
    //     {
    //         services.AddCors(options =>
    //         {
    //             options.AddPolicy(policyName, builder => 
    //                 builder.SetIsOriginAllowed(_ => true)
    //                     .AllowAnyMethod()
    //                     .AllowAnyHeader()
    //                     .WithExposedHeaders("X-Pagination"));
    //         });
    //     }
    //     else
    //     {
    //         //TODO update origins here with env vars or secret
    //         //services.AddCors(options =>
    //         //{
    //         //    options.AddPolicy(policyName, builder =>
    //         //        builder.WithOrigins(origins)
    //         //        .AllowAnyMethod()
    //         //        .AllowAnyHeader()
    //         //        .WithExposedHeaders("X-Pagination"));
    //         //});
    //     }
    // }
}