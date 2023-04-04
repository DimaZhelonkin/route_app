using System.Runtime.CompilerServices;
using Ark.Application.Entities;
using Ark.SharedLib.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

[assembly: InternalsVisibleTo("Astrum.Tests")]

namespace Ark.Application;

public class EventStoreDbContext : BaseDbContext
{
    public EventStoreDbContext(DbContextOptions<EventStoreDbContext> options) : base(options)
    {
    }

    public DbSet<Event> Events { get; set; }
    public DbSet<AggregateSnapshot> Snapshots { get; set; }
    public DbSet<BranchPoint> BranchPoints { get; set; }

    protected override void OnModelCreating(ModelBuilder builder) => base.OnModelCreating(builder);
}