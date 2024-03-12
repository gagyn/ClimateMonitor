using ClimateMonitor.Domain.Entities;

namespace ClimateMonitor.Domain.Repositories;

public interface ISensorConfigurationRepository : IBaseRepository
{
    Task<SensorConfigurationEntity> FindOrThrow(Guid sensorId, CancellationToken cancellationToken);
    void Add(SensorConfigurationEntity record);
}