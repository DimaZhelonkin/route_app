using Ark.Infrastructure.Shared.Culture.Providers;

namespace Ark.SharedLib.Api.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLocalisation(this IServiceCollection services) =>
        services.AddLocalization();

    public static IApplicationBuilder UseApplicationLocalisation(this IApplicationBuilder app,
        IConfiguration configuration)
    {
        var supportedCultures = new[] {"en", "ru"}; // TODO to options
        var localizationOptions = new RequestLocalizationOptions()
                                  .SetDefaultCulture(supportedCultures[0])
                                  .AddSupportedCultures(supportedCultures)
                                  .AddSupportedUICultures(supportedCultures);
        localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
        localizationOptions.RequestCultureProviders.Insert(0, new UserProfileRequestCultureProvider());
        app.UseRequestLocalization(localizationOptions);

        return app;
    }
}