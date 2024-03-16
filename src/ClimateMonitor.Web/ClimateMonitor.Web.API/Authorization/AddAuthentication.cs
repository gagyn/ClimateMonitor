using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;

namespace ClimateMonitor.Web.API.Authorization;

public static class AddAuthentication
{
    public static IServiceCollection AddAuthenticationWithBearer(this IServiceCollection serviceCollection)
    {
        // https://learn.microsoft.com/en-us/aspnet/core/signalr/authn-and-authz?view=aspnetcore-8.0#built-in-jwt-authentication
        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            // Configure the Authority to the expected value for
            // the authentication provider. This ensures the token
            // is appropriately validated.
            options.Authority = "Authority URL"; // TODO: Update URL

            // We have to hook the OnMessageReceived event in order to
            // allow the JWT authentication handler to read the access
            // token from the query string when a WebSocket or 
            // Server-Sent Events request comes in.

            // Sending the access token in the query string is required when using WebSockets or ServerSentEvents
            // due to a limitation in Browser APIs. We restrict it to only calls to the
            // SignalR hub in this code.
            // See https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
            // for more information about security considerations when using
            // the query string to transmit the access token.
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });
        serviceCollection.AddSingleton<IUserIdProvider, DeviceIdProvider>();

        return serviceCollection;
    }
}

public class DeviceIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection) => connection.User?.Identity?.Name;
}