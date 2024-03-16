namespace ClimateMonitor.Application.Authorization;

public interface IUserContext
{
    string UserName { get; }
    Guid? DeviceId { get; }
}