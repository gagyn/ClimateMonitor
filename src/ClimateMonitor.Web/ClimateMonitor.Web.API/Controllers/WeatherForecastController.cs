using ClimateMonitor.Web.API.Hubs;
using ClimateMonitor.Web.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ClimateMonitor.Web.API.Controllers;
[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IHubContext<SensorConfigurationHub> hubContext;
    private readonly SensorConfigurationService sensorConfigurationService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IHubContext<SensorConfigurationHub> hubContext)
    {
        _logger = logger;
        this.hubContext = hubContext;
        sensorConfigurationService = new();
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("send-message")]
    public async Task SendTestMessage()
    {
        var config = sensorConfigurationService.GetConfiguration(SensorConfigurationService.TestDeviceId);
        await hubContext.Clients.All.SendAsync("UpdateConfiguration", config);
    }
}
