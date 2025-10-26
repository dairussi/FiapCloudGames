using FiapCloudGames.Domain.Common.Entities;
using FiapCloudGames.Domain.Common.Ports;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FiapCloudGames.Infraestructure.Persistence.Interceptors;

public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly IUserContext? _userContext;

    public AuditInterceptor(IUserContext? userContext = null)
    {
        _userContext = userContext;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateAuditFields(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        UpdateAuditFields(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateAuditFields(DbContext? context)
    {
        if (context == null) return;

        var entries = context.ChangeTracker.Entries<BaseEntity>()
            .Where(e => e.State == EntityState.Added);

        foreach (var entry in entries)
        {
            if (_userContext != null)
            {
                try
                {
                    entry.Entity.CreatedBy = _userContext.GetCurrentUserId();
                }
                catch (UnauthorizedAccessException)
                {
                    entry.Entity.CreatedBy = 0;
                }
            }
        }
    }
}