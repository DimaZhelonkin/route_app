using System.Net;
using Ardalis.RouteAndBodyModelBinding;
using Ark.Infrastructure.Middlewares;
using Ark.StronglyTypedIds;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Newtonsoft.Json.Serialization;

namespace Ark.SharedLib.Api.Extensions;

public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddCustomizedMvc(this IServiceCollection services)
    {
        services
            .AddControllers(options =>
            {
                options.UseRoutePrefix("api");
                options.ModelBinderProviders.InsertRouteAndBodyBinding();
                // options.Conventions.Add(new DashedRoutingConvention());
                options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
                options.Filters.Add<ErrorHandlerFilterAttribute>();
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.Converters.Add(new StronglyTypedIdNewtonsoftJsonConverter());
            })
            // .AddJsonOptions(options =>
            // {
            //     options.JsonSerializerOptions.Converters.Add(new StronglyTypedIdJsonConverterFactory());
            // })
            // .AddDataAnnotationsLocalization(options =>
            // {
            //     options.DataAnnotationLocalizerProvider = (type, factory) =>
            //     {
            //         var assemblyName = new AssemblyName(typeof(SharedResources).GetTypeInfo().Assembly.FullName);
            //         return factory.Create("SharedResources", assemblyName.Name);
            //     };
            // })
            // .LoadModules(GlobalConfiguration.Modules)
            ; // TODO to test
        services.AddEndpointsApiExplorer();

        return services;
    }

    public static IServiceCollection AddApplicationCookie(this IServiceCollection services)
    {
        var cookieAuthenticationEvents = new CookieAuthenticationEvents
        {
            OnRedirectToLogin = async context =>
            {
                // TODO to api.domain.ru/v1/...
                if (context.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase) &&
                    context.Response.StatusCode == (int)HttpStatusCode.OK)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await context.Response.WriteAsync("test response with exception"); // TODO 
                }
            },
            OnRedirectToAccessDenied = context =>
            {
                if (context.Request.Path.StartsWithSegments("/api", StringComparison.OrdinalIgnoreCase) &&
                    context.Response.StatusCode == (int)HttpStatusCode.OK)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    return Task.CompletedTask;
                }

                return Task.CompletedTask;
            },
        };

        services.ConfigureApplicationCookie(options =>
        {
            options.ForwardAuthenticate = JwtBearerDefaults.AuthenticationScheme;
            // options.AccessDeniedPath = "/Shared/AccessDenied";
            // options.Cookie.Name = "Astrum.AUTH";
            // options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            // options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            // options.SlidingExpiration = true;
            // options.LoginPath = new PathString("/auth/login");
            // options.LogoutPath = new PathString("/logoff");
            options.Events = cookieAuthenticationEvents;
        });
        return services;
    }
}