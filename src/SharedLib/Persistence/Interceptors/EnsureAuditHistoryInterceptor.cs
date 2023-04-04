using Ark.IdentityServer.Application.Contracts;
using Ark.SharedLib.Application.Identity;
using Ark.SharedLib.Persistence.Extensions;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ark.SharedLib.Persistence.Interceptors;

public sealed class EnsureAuditHistoryInterceptor : SaveChangesInterceptor
{
    private readonly ICurrentUserService _currentUserService;

    public EnsureAuditHistoryInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var dbContext = eventData.Context;
        if (_currentUserService.UserId is not null)
        {
            dbContext?.EnsureAuditHistory(_currentUserService.Username);
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}