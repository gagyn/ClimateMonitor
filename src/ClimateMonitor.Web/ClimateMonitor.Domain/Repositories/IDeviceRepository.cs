using ClimateMonitor.Domain.Entities;

namespace ClimateMonitor.Domain.Repositories;

public interface IDeviceRepository : IBaseRepository
{
    Task<DeviceEntity> FindOrThrow(Guid deviceId, CancellationToken cancellationToken);
    void Add(DeviceEntity entity);
}
