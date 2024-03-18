namespace ClimateMonitor.Application.Authorization;

public interface IUserContext
{
    string UserName { get; }
    Guid Id { get; }
    Role Role { get; }
}