using Ark.Account.Extensions;
using Ark.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Ark.Account;

public class ModuleInitializer : IModuleInitializer
{
    #region IModuleInitializer Members

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);
        services.AddPersistenceServices(configuration);
        services.AddBackofficeServices();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
    }

    #endregion
}