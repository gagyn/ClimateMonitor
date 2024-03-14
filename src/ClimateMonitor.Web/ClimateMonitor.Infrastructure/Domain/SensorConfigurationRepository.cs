using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Domain.Repositories;
using ClimateMonitor.Infrastructure.Abstractions;
using ClimateMonitor.Infrastructure.Database;
using ClimateMonitor.Infrastructure.Extensions;

namespace ClimateMonitor.Infrastructure.Domain;

public class SensorConfigurationRepository(ClimateDbContext dbContext) : BaseRepository(dbContext), ISensorConfigurationRepository
{
    public Task<SensorConfigurationEntity> FindOrThrow(Guid sensorId, CancellationToken cancellationToken)
        => dbContext.SensorConfigurations.FindOrThrow(sensorId, cancellationToken);

    public void Add(SensorConfigurationEntity entity)
        => dbContext.SensorConfigurations.Add(entity);
}