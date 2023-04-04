using Ark.Application.Extensions;
using Ark.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Ark.Application;

public class ModuleInitializer : IModuleInitializer
{
    #region IModuleInitializer Members

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddPersistenceServices(configuration);
        services.AddInfrastructureServices(configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }

    #endregion
}