using ClimateMonitor.Web.API.Services;
using Microsoft.AspNetCore.SignalR;

namespace ClimateMonitor.Web.API.Hubs;

public class SensorConfigurationHub : Hub
{
    private readonly SensorConfigurationService sensorConfigurationService = new();

    public async Task GetConfiguration(Guid deviceId)
    {
        var sensorConfiguration = sensorConfigurationService.GetConfiguration(deviceId);
        await Clients.All.SendAsync("UpdateConfiguration", sensorConfiguration);
    }
}
