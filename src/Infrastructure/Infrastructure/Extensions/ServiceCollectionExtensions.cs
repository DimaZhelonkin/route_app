using System.Reflection;
using Ark.Infrastructure.BackgroundJobs;
using Ark.Infrastructure.Caching;
using Ark.Infrastructure.Helpers;
using Ark.Infrastructure.Idempotence;
using Ark.Infrastructure.Mappings;
using Ark.Infrastructure.MessageBroker;
using Ark.Infrastructure.Modules;
using Ark.Infrastructure.Services;
using Ark.Infrastructure.Storage;
using Ark.SharedLib.Application.Abstractions;
using Ark.SharedLib.Application.Abstractions.Shared;
using Ark.SharedLib.Common.Options;
using AutoMapper.Extensions.ExpressionMapping;
using Hangfire;
using Hangfire.PostgreSql;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Quartz;


namespace Ark.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var currentAssembly = typeof(InitialisationNotificationHandler).Assembly;
        services.AddMediatR(x =>
            x.RegisterServicesFromAssembly(currentAssembly)
        );
        services.AddMessageBroker(configuration);

        // services.AddModules();
        services.AddModulesServices(configuration);

        services.AddDbContext<ApplicationDbContext>(x =>
            x.UseNpgsql(configuration.GetConnectionString("ApplicationDbConnection")!));
        services.AddAutoMapper(cfg =>
        {
            cfg.AddExpressionMapping();
            cfg.AddMaps(typeof(AppProfile));
        });

        services.TryDecorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>));

        services.AddSingleton<ICacheService, CacheService>();

        services.AddHangfire(cfg =>
            cfg
                .UsePostgreSqlStorage(configuration.GetConnectionString("Hangfire.PostgreSQL"))
                .UseSerilogLogProvider()
                .UseRecommendedSerializerSettings()
        );

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
            configure
                .AddJob<ProcessOutboxMessagesJob>(jobKey)
                .AddTrigger(trigger =>
                    trigger.ForJob(jobKey)
                           .WithSimpleSchedule(schedule =>
                               schedule.WithIntervalInSeconds(10)
                                       .RepeatForever()
                           )
                );
            configure.UseMicrosoftDependencyInjectionJobFactory();
        });
        // TODO to startup project???
        services.AddQuartzHostedService();

        services.AddCaching(configuration);
        return services;
    }

    private static IServiceCollection AddCaching(this IServiceCollection services, IConfiguration configuration) =>
        services.AddStackExchangeRedisCache(options => configuration
                                                       .GetRequiredSection("RedisCache")
                                                       .Bind(options));

    public static IServiceCollection AddMessageBroker(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageBrokerSettings>(configuration.GetSection(MessageBrokerSettings.Section));
        services.AddSingleton<MessageBrokerSettings>(sp =>
            sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);
        // services.AddOptions<MessageBrokerSettings>();
        services.AddMassTransit(cfg =>
        {
            cfg.SetKebabCaseEndpointNameFormatter();
            //Чтобы работала обработка запросов надо поставить расширение на RabbitMq rabbitmq_delayed_message_exchange
            cfg.AddDelayedMessageScheduler();

            //Тут регистрируем наши обработчики сообщений
            // cfg.AddConsumer<AddMoneyConsumer>();
            // cfg.AddConsumer<GetMoneyConsumer>();

            cfg.UsingRabbitMq((brc, rbfc) =>
            {
                //Использовать паттерн OutBox - либо все сообщений одной пачкой сразу отправляются 
                //либо не будет отправлено ни одно из сообщений. 
                //Это нужно, когда вам, например, нужно послать две команды сразу CreateOrder 
                // и SendEmail только при условии, что отправятся оба либо ни одно из них.
                rbfc.UseInMemoryOutbox();
                rbfc.UseMessageRetry(r =>
                {
                    //Повторять 3 раза каждый раз увеличивая между повторами 
                    //интервал на 1 секунду. Начать с интервала в 1 секунду.
                    r.Incremental(3, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
                });
                //Использовать отложенные сообщения в том числе с помощью них можно 
                //делать таймауты
                rbfc.UseDelayedMessageScheduler();
                var settings = brc.GetRequiredService<MessageBrokerSettings>();
                rbfc
                    .Host(settings.Host, settings.Port, settings.VirtualHost, h =>
                    {
                        h.Username(settings.Username);
                        h.Password(settings.Password);
                    });
                //Записываем какие сообщения мы слушаем. Вызывать этот метод обязательно
                //иначе обработчики не будут реагировать на сообщения.
                rbfc.ConfigureEndpoints(brc);
            });
        });

        services.AddTransient<IEventBus, EventBus>();
        services.AddSingleton<IPasswordGenerator, PasswordGenerator>();

        return services;
    }


    private static IServiceCollection AddModulesServices(this IServiceCollection services,
        IConfiguration configurationManager)
    {
        var assemblyLocation = Assembly.GetExecutingAssembly().Location;
        var assemblyFolder = Path.GetDirectoryName(assemblyLocation);
        var files = Directory.GetFiles(assemblyFolder!, "*.Startup.dll");
        var loadedStartupProjects = files.Select(Assembly.LoadFile).ToList();
        var moduleInitializerTypes = loadedStartupProjects
                                     .Select(x => x.GetTypes()
                                                   .FirstOrDefault(t => typeof(IModuleInitializer).IsAssignableFrom(t))
                                     )
                                     .Where(x => x is not null)
                                     .ToList();
        foreach (var moduleInitializerType in moduleInitializerTypes)
        {
            var moduleInitializer = (IModuleInitializer)Activator.CreateInstance(moduleInitializerType!)!;
            services.AddSingleton(typeof(IModuleInitializer), moduleInitializer);
            moduleInitializer.ConfigureServices(services, configurationManager);
        }

        return services;
    }

    private static IServiceCollection
        AddStorageOptions(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<FileStorageOptions>(x => configuration.GetSection("Storage").Bind(x));

    private static IServiceCollection AddEmailOptions(this IServiceCollection services, IConfiguration configuration) =>
        services.Configure<EmailOptions>(x => configuration.GetSection("Email").Bind(x));


    /// <summary>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var audience = configuration["Authentication:JwtBearer:Audience"];
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Events.OnRedirectToAccessDenied =
                        options.Events.OnRedirectToLogin = c =>
                        {
                            c.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            return Task.FromResult<object>(null);
                        };
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["Authentication:JwtBearer:Authority"];
                    options.Audience = audience;
                    // options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // Validate the JWT Issuer (iss) claim
                        // ValidateIssuer = true,
                        // ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],

                        // The signing key must match!
                        // ValidateIssuerSigningKey = true,
                        // IssuerSigningKey =
                        // new SymmetricSecurityKey(
                        // Encoding.UTF8.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"])),

                        // Validate the JWT Audience (aud) claim
                        ValidateAudience = false,
                        ValidAudience = audience,
                        ValidAudiences = new[] {audience},
                        // ValidTypes = new[] {"at+jwt"},
                        // Validate the token expiry
                        // ValidateLifetime = true
                        // If you want to allow a certain amount of clock drift, set that here
                        // ClockSkew = TimeSpan.Zero
                    };
                });
        // services.AddAuthentication(options =>
        //     {
        //         // custom scheme defined in .AddPolicyScheme() below
        //         options.DefaultScheme = "JWT_OR_COOKIE";
        //         options.DefaultChallengeScheme = "JWT_OR_COOKIE";
        //     })
        //     .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        //     {
        //         options.LoginPath = "/login";
        //         options.ExpireTimeSpan = TimeSpan.FromDays(1);
        //     })
        //     .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        //     {
        //         options.Authority = configuration["Authentication:JwtBearer:Authority"];
        //         options.Audience = audience;
        //         options.RequireHttpsMetadata = false;
        //         options.TokenValidationParameters = new TokenValidationParameters
        //         {
        //             // Validate the JWT Issuer (iss) claim
        //             ValidateIssuer = true,
        //             ValidIssuer = configuration["Authentication:JwtBearer:Issuer"],
        //
        //             // The signing key must match!
        //             // ValidateIssuerSigningKey = true,
        //             // IssuerSigningKey =
        //             // new SymmetricSecurityKey(
        //             // Encoding.UTF8.GetBytes(configuration["Authentication:JwtBearer:SecurityKey"])),
        //
        //             // Validate the JWT Audience (aud) claim
        //             ValidateAudience = true,
        //             ValidAudience = audience,
        //             ValidAudiences = new[] {audience},
        //             ValidTypes = new[] {"at+jwt"},
        //             // Validate the token expiry
        //             ValidateLifetime = true
        //             // If you want to allow a certain amount of clock drift, set that here
        //             // ClockSkew = TimeSpan.Zero
        //         };
        //     })
        //     .AddPolicyScheme("JWT_OR_COOKIE", "JWT_OR_COOKIE", options =>
        //     {
        //         // runs on each request
        //         options.ForwardDefaultSelector = context =>
        //         {
        //             // filter by auth type
        //             string authorization = context.Request.Headers[HeaderNames.Authorization];
        //             if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
        //                 return JwtBearerDefaults.AuthenticationScheme;
        //
        //             // otherwise always check for cookie auth
        //             return CookieAuthenticationDefaults.AuthenticationScheme;
        //         };
        //     });
        // .AddGoogle(options =>
        // {
        //     options.SignInScheme = IdentityServerConstants.JwtRequestClientKey;
        //
        //     options.ClientId = configuration["Authentication:Google:ClientId"];
        //     options.ClientSecret = configuration["Authentication:Google:ClientSecret"];
        // });
        return services;
    }

    public static IServiceCollection AddCustomAuthorization(this IServiceCollection services,
        IConfiguration configuration)
    {
        var apiAudience = configuration["Authentication:JwtBearer:Audience"];
        services.AddAuthorization(options =>
        {
            options.AddPolicy("ApiScope", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("scope", apiAudience);
            });
        });
        return services;
    }
}