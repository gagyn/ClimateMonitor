using ClimateMonitor.Domain.Entities;

namespace ClimateMonitor.Domain.Repositories;

public interface IDeviceConfigurationRepository : IBaseRepository
{
    Task<DeviceConfigurationEntity> FindOrThrow(Guid deviceId, CancellationToken cancellationToken);
    void Add(DeviceConfigurationEntity entity);
}
