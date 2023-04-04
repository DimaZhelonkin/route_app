using Ark.SharedLib.Application.Abstractions.Repositories;
using Ark.SharedLib.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;


namespace Ark.SharedLib.Persistence.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomDbContext<TDbContext>(this IServiceCollection services,
        string connectionString,
        Action<NpgsqlDbContextOptionsBuilder>? npgsqlOptionsAction = null)
        where TDbContext : DbContext, IUnitOfWork
    {
        var moduleAssemblyName = typeof(TDbContext).Assembly.FullName;
        var schemaName = typeof(TDbContext).Name.Replace("DbContext", "");
        var migrationHistoryTableName = $"__{schemaName}_MigrationsHistory";
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();
        services.AddScoped<EnsureAuditHistoryInterceptor>();
        services.AddScoped<UpdateAuditableEntitiesInterceptor>();
        services.AddDbContext<TDbContext>((sp, options) =>
            {
                var convertDomainEventsToOutboxMessagesInterceptor =
                    sp.GetRequiredService<ConvertDomainEventsToOutboxMessagesInterceptor>();
                var ensureAuditHistoryInterceptor = sp.GetRequiredService<EnsureAuditHistoryInterceptor>();
                var entitiesAuditingInterceptor = sp.GetRequiredService<UpdateAuditableEntitiesInterceptor>();
                var interceptors = new IInterceptor[]
                {
                    convertDomainEventsToOutboxMessagesInterceptor,
                    entitiesAuditingInterceptor,
                    ensureAuditHistoryInterceptor,
                };
                options
                    .UseNpgsql(connectionString,
                        b =>
                        {
                            npgsqlOptionsAction?.Invoke(b);
                            b.MigrationsAssembly(moduleAssemblyName)
                             .MigrationsHistoryTable(migrationHistoryTableName, schemaName);
                        }
                    )
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .AddInterceptors(interceptors);

                // options.EnableSensitiveDataLogging();
            }
        );
        services.AddScoped<DbContext, TDbContext>();
        // services.AddScoped<IUnitOfWork, TDbContext>();
        // container?.RegisterConditional<IUnitOfWork, TDbContext>(Lifestyle.Scoped, InCurrentModule());
        return services;
    }

    // private static Predicate<PredicateContext> InCurrentModule() =>
    //     c =>
    //     {
    //         var assemblyName = c.Consumer.ImplementationType.Assembly.GetName().Name!;
    //         var assemblyNameParts = assemblyName.Split(".");
    //         if (assemblyNameParts.Length < 2) return false;
    //         var moduleName = assemblyNameParts[1];
    //         return assemblyName.Contains(moduleName);
    //     };
}