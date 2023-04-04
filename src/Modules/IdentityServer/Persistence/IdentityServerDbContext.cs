using Ark.IdentityServer.Domain.ApplicationUser;
using Ark.SharedLib.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Ark.IdentityServer.Persistence;

public class IdentityServerDbContext : BaseDbContext
{
    private const string PostGisExtensionName = "postgis";

    public IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) => base.OnModelCreating(builder);
    // builder.EnableDetailedErrors();
    // builder.UseLazyLoadingProxies();  
}