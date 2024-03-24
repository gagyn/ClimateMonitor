using ClimateMonitor.Domain.Entities;

namespace ClimateMonitor.Domain.Repositories;

public interface IUserRepository : IBaseRepository
{
    Task<UserEntity> FindOrThrow(Guid userId, CancellationToken cancellationToken);
    void Add(UserEntity entity);
}
