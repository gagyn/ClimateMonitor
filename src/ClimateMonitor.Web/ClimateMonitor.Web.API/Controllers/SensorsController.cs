using ClimateMonitor.Application.Commands;
using ClimateMonitor.Application.Queries;
using ClimateMonitor.Web.API.Authorization;
using ClimateMonitor.Web.API.Hubs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ClimateMonitor.Web.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SensorsController(ILogger<SensorsController> logger, IHubContext<SensorConfigurationHub> hubContext, IMediator mediator) : ControllerBase
{
    private readonly IMediator mediator = mediator;
    private readonly ILogger<SensorsController> logger = logger;
    private readonly IHubContext<SensorConfigurationHub> hubContext = hubContext;

    [Authorize(Policies.Device)]
    [HttpPost]
    public async Task<IActionResult> SaveRecord([FromBody] AddRecordCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }

    [Authorize(Policies.User)]
    [HttpGet("devices")]
    public async Task<IActionResult> GetDevices()
        => Ok(await mediator.Send(new GetDevicesQuery()));

    [HttpGet("configuration/{deviceId:guid}")]
    public async Task<IActionResult> FindConfiguration([FromRoute] Guid deviceId)
        => Ok(await mediator.Send(new FindConfigurationQuery(deviceId)));

    [Authorize(Policies.User)]
    [HttpPost("configuration/{deviceId}")]
    public async Task<IActionResult> AddSensorConfiguration(
        [FromRoute] Guid deviceId,
        [FromBody] AddSensorConfigurationCommand command)
    {
        await mediator.Send(command with
        {
            DeviceId = deviceId
        });
        return Ok();
    }
    
    [Authorize(Policies.User)]
    [HttpPut("configuration/{deviceId:guid}/{sensorId:guid}")]
    public async Task<IActionResult> UpdateSensorConfiguration(
        [FromRoute] Guid deviceId,
        [FromRoute] Guid sensorId,
        [FromBody] UpdateSensorConfigurationCommand command)
    {
        await mediator.Send(command with
        {
            DeviceId = deviceId,
            SensorId = sensorId
        });
        return Ok();
    }

    [HttpGet("send-message")]
    public async Task SendTestMessage()
    {
        var testDeviceId = Guid.Parse("a22e59b7-aa2b-49df-9420-97d6e017fbb3");
        var config = await mediator.Send(new FindConfigurationQuery(testDeviceId));
        await hubContext.Clients.All.SendAsync("UpdateConfiguration", config);
    }
}
