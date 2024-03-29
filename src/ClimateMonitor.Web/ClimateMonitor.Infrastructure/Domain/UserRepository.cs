using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Domain.Repositories;
using ClimateMonitor.Infrastructure.Abstractions;
using ClimateMonitor.Infrastructure.Database;
using ClimateMonitor.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace ClimateMonitor.Infrastructure.Domain;

public class UserRepository(ClimateDbContext dbContext) : BaseRepository(dbContext), IUserRepository
{
    public Task<UserEntity> FindOrThrow(Guid deviceId, CancellationToken cancellationToken)
        => dbContext.Users.Include(x => x.BaseUser)
            .FindOrThrow(x => x.Id, deviceId, cancellationToken);

    public void Add(UserEntity entity)
        => dbContext.Users.Add(entity);
}
