using ClimateMonitor.Application.Handlers;
using ClimateMonitor.Infrastructure.Handlers;
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

    [HttpPost]
    public async Task<IActionResult> SaveRecord([FromBody] AddRecordCommand command)
    {
        await mediator.Send(command);
        return Ok();
    }

    [HttpGet("send-message")]
    public async Task SendTestMessage()
    {
        var testDeviceId = Guid.Parse("a22e59b7-aa2b-49df-9420-97d6e017fbb3");
        var config = mediator.Send(new FindConfigurationQuery(testDeviceId));
        await hubContext.Clients.All.SendAsync("UpdateConfiguration", config);
    }
}
