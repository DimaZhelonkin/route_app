using Ark.Infrastructure.Modules;
using Ark.Rides.Application.Extensions;
using Ark.Rides.Backoffice.Extensions;
using Ark.Rides.Infrastructure.Extensions;
using Ark.Rides.Persistence.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Ark.Rides.Startup;

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