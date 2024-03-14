using ClimateMonitor.Domain.Entities;

namespace ClimateMonitor.Domain.Repositories;
public interface IRecordRepository : IBaseRepository
{
    Task<RecordEntity> FindOrThrow(int id, CancellationToken cancellationToken);
    void Add(RecordEntity entity);
}
