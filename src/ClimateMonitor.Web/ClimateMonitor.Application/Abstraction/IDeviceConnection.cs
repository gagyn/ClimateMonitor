using ClimateMonitor.Application.Models;

namespace ClimateMonitor.Application.Abstraction;
public interface IDeviceConnection
{
    Task SendUpdatedConfiguration(DeviceConfiguration deviceConfiguration);
}
