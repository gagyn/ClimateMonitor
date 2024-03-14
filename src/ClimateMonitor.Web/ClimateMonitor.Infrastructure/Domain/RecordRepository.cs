using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Domain.Repositories;
using ClimateMonitor.Infrastructure.Abstractions;
using ClimateMonitor.Infrastructure.Database;
using ClimateMonitor.Infrastructure.Extensions;

namespace ClimateMonitor.Infrastructure.Domain;

public class RecordRepository(ClimateDbContext dbContext) : BaseRepository(dbContext), IRecordRepository
{
    public Task<RecordEntity> FindOrThrow(int id, CancellationToken cancellationToken)
        => dbContext.Records.FindOrThrow(id, cancellationToken);

    public void Add(RecordEntity entity)
        => dbContext.Records.Add(entity);
}