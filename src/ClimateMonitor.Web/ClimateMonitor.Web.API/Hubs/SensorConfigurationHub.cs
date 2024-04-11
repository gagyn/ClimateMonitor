using ClimateMonitor.Application.Authorization;
using ClimateMonitor.Application.Queries;
using ClimateMonitor.Web.API.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ClimateMonitor.Web.API.Hubs;

[Authorize(Policies.Device)]
public class SensorConfigurationHub(IMediator mediator, IUserContext userContext) : Hub
{
    public async Task GetConfiguration()
    {
        var sensorConfiguration = await mediator.Send(new FindConfigurationQuery(userContext.Id));
        await Clients.Caller.SendAsync("UpdateConfiguration", sensorConfiguration);
    }
}
