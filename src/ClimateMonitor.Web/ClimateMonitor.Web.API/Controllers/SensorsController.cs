using ClimateMonitor.Web.API.Hubs;
using ClimateMonitor.Web.API.Models;
using ClimateMonitor.Web.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ClimateMonitor.Web.API.Controllers;
[ApiController]
[Route("[controller]")]
public class SensorsController(ILogger<SensorsController> logger, IHubContext<SensorConfigurationHub> hubContext) : ControllerBase
{
    private readonly ILogger<SensorsController> _logger = logger;
    private readonly IHubContext<SensorConfigurationHub> hubContext = hubContext;
    private readonly SensorConfigurationService sensorConfigurationService = new();

    [HttpPost]
    public async Task SaveRecord([FromBody] Record record)
    {

    }

    [HttpGet("send-message")]
    public async Task SendTestMessage()
    {
        var config = sensorConfigurationService.GetConfiguration(SensorConfigurationService.TestDeviceId);
        await hubContext.Clients.All.SendAsync("UpdateConfiguration", config);
    }
}
