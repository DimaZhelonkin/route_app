using Ark.SharedLib.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Ark.Account;

public class AccountDbContext : BaseDbContext
{
    public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder) => base.OnModelCreating(builder);
}