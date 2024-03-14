using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Domain.Repositories;
using ClimateMonitor.Infrastructure.Abstractions;
using ClimateMonitor.Infrastructure.Database;
using ClimateMonitor.Infrastructure.Extensions;

namespace ClimateMonitor.Infrastructure.Domain;

public class DeviceConfigurationRepository(ClimateDbContext dbContext) : BaseRepository(dbContext), IDeviceConfigurationRepository
{
    public Task<DeviceConfigurationEntity> FindOrThrow(Guid deviceId, CancellationToken cancellationToken)
        => dbContext.DeviceConfigurations.FindOrThrow(deviceId, cancellationToken);

    public void Add(DeviceConfigurationEntity entity)
        => dbContext.DeviceConfigurations.Add(entity);
}