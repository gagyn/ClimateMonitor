using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace ClimateMonitor.Web.API.Controllers;

record SensorConfiguration(
    Dictionary<Guid, string> ReadingFrequencyCrons,
    Dictionary<Guid, int> PinsDHT11,
    Dictionary<Guid, int> PinsDHT22,
    Dictionary<Guid, int> PinsDallas18b20);

[ApiExplorerSettings(IgnoreApi = true)]
public class WebSocketController : ControllerBase
{
    private readonly static Guid sensorId = Guid.NewGuid();
    private readonly static Guid deviceId = Guid.Parse("a22e59b7-aa2b-49df-9420-97d6e017fbb3");
    private static SensorConfiguration sensorConfig = new(
        new Dictionary<Guid, string>
        {
            { Guid.NewGuid(), "* * * * *" }, // Example CRON expression
            { Guid.NewGuid(), "0 * * * *" }  // Another example CRON expression
        },
        new Dictionary<Guid, int>
        {
            { Guid.NewGuid(), 4 },  // Example pin configuration for DHT11 sensor
            { Guid.NewGuid(), 17 }  // Another example pin configuration for DHT11 sensor
        },
        new Dictionary<Guid, int>
        {
            { Guid.NewGuid(), 23 },  // Example pin configuration for DHT22 sensor
            { Guid.NewGuid(), 24 }   // Another example pin configuration for DHT22 sensor
        },
        new Dictionary<Guid, int>
        {
            { Guid.NewGuid(), 5 },  // Example pin configuration for Dallas 18b20 sensor
            { Guid.NewGuid(), 6 }   // Another example pin configuration for Dallas 18b20 sensor
        }
    );
    private static readonly Dictionary<Guid, SensorConfiguration> deviceConfigurations = new()
    {
        {deviceId, sensorConfig}
    };
    
    [Route("/ws")]
    public async Task Get()
    {
        if (HttpContext.WebSockets.IsWebSocketRequest)
        {
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            await Echo(webSocket);
        }
        else
        {
            HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }

    private static Task SendSensorConfiguration(WebSocket webSocket, Guid deviceId)
    {
        // todo: https://learn.microsoft.com/en-us/aspnet/core/signalr/configuration?view=aspnetcore-8.0&tabs=dotnet
        var sensorConfiguration = deviceConfigurations[deviceId];
        return Task.CompletedTask;
    }
    
    private static async Task Echo(WebSocket webSocket)
    {
        var buffer = new byte[1024 * 4];
        var receiveResult = await webSocket.ReceiveAsync(
            new ArraySegment<byte>(buffer), CancellationToken.None);

        while (!receiveResult.CloseStatus.HasValue)
        {
            await webSocket.SendAsync(
                new ArraySegment<byte>(buffer, 0, receiveResult.Count),
                receiveResult.MessageType,
                receiveResult.EndOfMessage,
                CancellationToken.None);

            receiveResult = await webSocket.ReceiveAsync(
                new ArraySegment<byte>(buffer), CancellationToken.None);
        }

        await webSocket.CloseAsync(
            receiveResult.CloseStatus.Value,
            receiveResult.CloseStatusDescription,
            CancellationToken.None);
    }

}