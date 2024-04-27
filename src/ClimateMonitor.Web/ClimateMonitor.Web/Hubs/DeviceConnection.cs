using ClimateMonitor.Application.Abstraction;
using ClimateMonitor.Application.Models;
using Microsoft.AspNetCore.SignalR;

namespace ClimateMonitor.Web.Hubs;

public class DeviceConnection(IHubContext<SensorConfigurationHub, ISensorConfigurationClient> hubContext) : IDeviceConnection
{
    private readonly IHubContext<SensorConfigurationHub, ISensorConfigurationClient> hubContext = hubContext;

    public Task SendUpdatedConfiguration(DeviceConfiguration deviceConfiguration)
        => hubContext.Clients.User(deviceConfiguration.DeviceId.ToString()).ConfigurationRecieved(deviceConfiguration);
}
