using Ark.Core.Extensions;
using Ark.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Ark.Core;

public class ModuleInitializer : IModuleInitializer
{
    #region IModuleInitializer Members

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddPersistenceServices(configuration);
        services.AddInfrastructureServices(configuration);
        services.AddBackofficeServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }

    #endregion
}