namespace ClimateMonitor.Domain.Repositories;

public interface IBaseRepository
{
    Task SaveChanges(CancellationToken cancellationToken);
}