using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClimateMonitor.Infrastructure.Abstractions;

public abstract class BaseRepository(DbContext dbContext) : IBaseRepository
{
    public async Task SaveChanges(CancellationToken cancellationToken)
        => await dbContext.SaveChangesAsync(cancellationToken);
}