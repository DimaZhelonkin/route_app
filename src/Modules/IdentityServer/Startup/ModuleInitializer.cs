using Ark.IdentityServer.Application.Extensions;
using Ark.IdentityServer.DomainServices.Extensions;
using Ark.IdentityServer.Infrastructure.Extensions;
using Ark.IdentityServer.Persistence.Extensions;
using Ark.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Astrum.IdentityServer.Startup;

public class ModuleInitializer : IModuleInitializer
{
    #region IModuleInitializer Members

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices(configuration);
        services.AddPersistenceServices(configuration);
        services.AddInfrastructureServices(configuration);
        // services.AddBackofficeServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }

    #endregion
}