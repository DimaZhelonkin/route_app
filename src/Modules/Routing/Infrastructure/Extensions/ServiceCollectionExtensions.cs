using System.Net.Http.Headers;
using System.Reflection;
using Ark.Routing.Contracts;
using Ark.Routing.HttpClients.VkClient;
using Ark.Routing.Mappings;
using Ark.Routing.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ark.Routing.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(x =>
            x.RegisterServicesFromAssembly(currentAssembly)
        );
        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(typeof(RoutingProfile).Assembly);
        });
        services.AddTransient<IRoutingProvider, RoutingProvider>();
        services.Decorate(typeof(IRoutingProvider), typeof(CachedRoutingProvider));
        var vkRoutingApiSection = configuration.GetRequiredSection(VkRoutingApiOptions.VkRoutingApi);
        services.Configure<VkRoutingApiOptions>(x => vkRoutingApiSection.Bind(x));
        var vkRoutingApiOptions = vkRoutingApiSection.Get<VkRoutingApiOptions>();
        // Register HttpClient
        services.AddHttpClient<IVkRoutingClient, VkRoutingClient>(x =>
        {
            
            x.BaseAddress = new Uri(vkRoutingApiOptions.Uri);
            x.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });
        // Register Logging for HttpClient
        services.AddHttpClientLogging(configuration);
        services.EnableLoggingForBadRequests();
        // services.AddHeaderPropagation(options => { options.HeaderNames.Add("X-My-Header"); });
        return services;
    }
}