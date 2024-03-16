using ClimateMonitor.Application.Authorization;
using ClimateMonitor.Infrastructure.Handlers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ClimateMonitor.Web.API.Hubs;

[Authorize(Roles = nameof(Role.Device))]
public class SensorConfigurationHub(IMediator mediator, IUserContext userContext) : Hub
{
    public async Task GetConfiguration()
    {
        var sensorConfiguration = mediator.Send(new FindConfigurationQuery(userContext.DeviceId!.Value)); // deviceId is never null in this case
        await Clients.Caller.SendAsync("UpdateConfiguration", sensorConfiguration);
    }
}
