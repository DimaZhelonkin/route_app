namespace Ark.SharedLib.Persistence.Abstractions;

public interface IDbContextInitializer
{
    Task InitAsync(bool migrate = true, bool seed = true, CancellationToken cancellationToken = default);
    Task MigrateAsync(CancellationToken cancellationToken = default);
    Task SeedAsync(CancellationToken cancellationToken = default);
}