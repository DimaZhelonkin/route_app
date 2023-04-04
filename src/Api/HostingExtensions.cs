using Ark.Infrastructure.Extensions;
using Ark.Infrastructure.Modules;
using Ark.SharedLib.Api.Extensions;
using Ark.SharedLib.Api.Logging;
using Ark.SharedLib.Api.Middlewares;
using Ark.SharedLib.Application.Extensions;
using Ark.SharedLib.Common;
using FluentValidation;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;
using Serilog.Events;


namespace Ark.Api;

static internal class HostingExtensions
{

    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        services.AddHttpContextAccessor();
        //ROUTING
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            // options.LowercaseQueryStrings = true;
            options.AppendTrailingSlash = true;
            options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
        });
        services.AddApplicationLocalisation();
        services.AddResponseCaching();
        services.AddApplicationSwagger(configuration);

        // TODO specify after docker-compose initiating
        services.AddCors(options =>
        {
            // this defines a CORS policy called "default"
            options.AddPolicy(Cors.Policy, policy =>
            {
                // policy.WithOrigins("https://localhost:5003")
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });
        // services.AddCustomAuthentication(Configuration);
        // services.AddCustomAuthorization(Configuration);
        services.AddCustomizedMvc();

        services.AddApplicationServices();
        services.AddInfrastructureServices(configuration);
        // services.AddPersistenceLayer(configuration);
        services.AddValidatorsFromAssemblyContaining<Program>();

        // services.AddScoped<UserHelper>();

        // services.AddScoped<SlugRouteValueTransformer>();
        // services.AddSingleton(services);
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressInferBindingSourcesForParameters = true); // TODO to each module?

        services.AddHttpLogging(logging =>
        {
            logging.LoggingFields = HttpLoggingFields.All;
            // logging.RequestHeaders.Add("sec-ch-ua");
            // logging.ResponseHeaders.Add("MyResponseHeader");
            // logging.MediaTypeOptions.AddText("application/javascript");
            logging.RequestBodyLogLimit = 4096;
            logging.ResponseBodyLogLimit = 4096;
        });
        services.AddApplicationCookie();
        services.Configure<JwtBearerOptions>(
            options =>
            {
                var onTokenValidated = options.Events.OnTokenValidated;

                options.Events.OnTokenValidated = async context =>
                {
                    await onTokenValidated(context);
                };
            });
        services.AddHealthChecks();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        var env = app.Environment;
        var configuration = app.Configuration;

        // Service locator
        ServicesProvider.Init(app.Services);
        if (env.IsDevelopment() || env.EnvironmentName == "DockerCompose")
        {
            app.UseDeveloperExceptionPage();
            app.UseApplicationSwagger(configuration);
        }

        app.UseHttpLogging();
        app.UseMiddleware<RequestLogContextMiddleware>();
        app.UseSerilogRequestLogging(options =>
        {
            // Customize the message template
            // options.MessageTemplate = "Handled {RequestPath}";

            // Emit debug-level events instead of the defaults
            options.GetLevel = (httpContext, elapsed, ex) => LogEventLevel.Debug;

            // Attach additional properties to the request completion event
            options.EnrichDiagnosticContext = LogEnricher.EnrichFromRequest;
        });

        var basePath = configuration.GetValue<string>("BasePath");
        if (!string.IsNullOrWhiteSpace(basePath))
            app.UsePathBase(basePath);

        app.UseHangfireDashboard();

        app.UseCors(Cors.Policy);
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseApplicationLocalisation(configuration);
        app.UseResponseCaching();

        var moduleInitializers = app.Services.GetServices<IModuleInitializer>();
        foreach (var moduleInitializer in moduleInitializers)
            moduleInitializer.Configure(app, env);

        app.MapControllers();
        app.MapHangfireDashboard();
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResultStatusCodes =
            {
                [HealthStatus.Healthy] = StatusCodes.Status200OK,
                [HealthStatus.Degraded] = StatusCodes.Status200OK,
                [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable,
            },
        });
        return app;
    }
}