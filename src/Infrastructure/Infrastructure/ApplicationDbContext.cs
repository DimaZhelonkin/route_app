using Ark.SharedLib.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ark.Infrastructure;

// TODO костыль перенести это в отдельную сборку
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<OutboxMessageConsumer> OutboxMessageConsumers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<OutboxMessageConsumer>()
                    .HasKey(m => new {m.Id, m.Name});
    }
}