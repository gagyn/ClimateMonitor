using ClimateMonitor.Domain.Entities;

namespace ClimateMonitor.Domain.Repositories;

public interface IDeviceUserRepository : IBaseRepository
{
    Task<DeviceUserEntity> FindOrThrow(Guid deviceId, CancellationToken cancellationToken);
    void Add(DeviceUserEntity entity);
}