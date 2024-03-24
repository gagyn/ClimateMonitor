using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Domain.Repositories;
using ClimateMonitor.Infrastructure.Abstractions;
using ClimateMonitor.Infrastructure.Database;
using ClimateMonitor.Infrastructure.Extensions;

namespace ClimateMonitor.Infrastructure.Domain;

public class DeviceUserRepository(ClimateDbContext dbContext) : BaseRepository(dbContext), IDeviceUserRepository
{
    public Task<DeviceUserEntity> FindOrThrow(Guid deviceId, CancellationToken cancellationToken)
        => dbContext.DeviceUsers.FindOrThrow(deviceId, cancellationToken);

    public void Add(DeviceUserEntity entity)
        => dbContext.DeviceUsers.Add(entity);
}