using ClimateMonitor.Application.Authorization;
using ClimateMonitor.Application.Models;
using ClimateMonitor.Application.Queries;
using ClimateMonitor.Web.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ClimateMonitor.Web.Hubs;

[Authorize(Policies.Device)]
public class SensorConfigurationHub(IMediator mediator, IUserContext userContext) : Hub<ISensorConfigurationClient>
{
    public override async Task OnConnectedAsync()
    {
        await GetConfiguration();
        await base.OnConnectedAsync();
    }

    public async Task GetConfiguration()
    {
        var sensorConfiguration = await mediator.Send(new FindConfigurationQuery(userContext.Id));
        await Clients.Caller.ConfigurationRecieved(sensorConfiguration);
    }
}

public interface ISensorConfigurationClient
{
    Task ConfigurationRecieved(DeviceConfiguration deviceConfiguration);
}