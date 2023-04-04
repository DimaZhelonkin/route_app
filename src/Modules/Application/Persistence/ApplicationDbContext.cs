using System.Runtime.CompilerServices;
using Ark.Application.Aggregates;
using Ark.SharedLib.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

[assembly: InternalsVisibleTo("Astrum.Tests")]

namespace Ark.Application;

public class ApplicationDbContext : BaseDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationConfiguration> ApplicationConfigurations { get; set; }
}