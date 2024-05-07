using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Net.Http.Headers;

namespace ClimateMonitor.Web.Authorization;

public static class AuthenticationExtensions
{
    private const string BearerOrCookieSchema = "BearerOrCookie";
    
    public static IServiceCollection AddAuthenticationWithBearer(this IServiceCollection serviceCollection)
    {
        // https://stackoverflow.com/questions/77936043/asp-net-core-8-default-identity-token-based-auth-not-working-404-not-found-err
        // https://learn.microsoft.com/en-us/aspnet/core/signalr/authn-and-authz?view=aspnetcore-8.0#built-in-jwt-authentication
        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = BearerOrCookieSchema;
            options.DefaultChallengeScheme = BearerOrCookieSchema;
        })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddBearerToken(IdentityConstants.BearerScheme, options => options.Events = new()
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
            }).AddPolicyScheme(BearerOrCookieSchema, BearerOrCookieSchema, options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    string? authorization = context.Request.Headers[HeaderNames.Authorization];
                    if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                        return IdentityConstants.BearerScheme;
                
                    return CookieAuthenticationDefaults.AuthenticationScheme;
                };

            });
        //    .AddJwtBearer(options =>
        //{
        //    // Configure the Authority to the expected value for
        //    // the authentication provider. This ensures the token
        //    // is appropriately validated.
        //    options.Authority = "https://localhost:7248"; // TODO: Update URL

        //    // We have to hook the OnMessageReceived event in order to
        //    // allow the JWT authentication handler to read the access
        //    // token from the query string when a WebSocket or 
        //    // Server-Sent Events request comes in.

        //    // Sending the access token in the query string is required when using WebSockets or ServerSentEvents
        //    // due to a limitation in Browser APIs. We restrict it to only calls to the
        //    // SignalR hub in this code.
        //    // See https://docs.microsoft.com/aspnet/core/signalr/security#access-token-logging
        //    // for more information about security considerations when using
        //    // the query string to transmit the access token.
        //    options.Events = new JwtBearerEvents
        //    {
        //        OnMessageReceived = context =>
        //        {
        //            var accessToken = context.Request.Query["access_token"];
        //            if (!string.IsNullOrEmpty(accessToken))
        //            {
        //                context.Token = accessToken;
        //            }
        //            return Task.CompletedTask;
        //        }
        //    };
        //});
        serviceCollection.AddSingleton<IUserIdProvider, DeviceIdProvider>();

        serviceCollection.AddIdentityCore<BaseUserEntity>(options =>
        {
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
        })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ClimateDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager()
            .AddApiEndpoints();

        return serviceCollection;
    }
}

public class DeviceIdProvider : IUserIdProvider
{
    public string? GetUserId(HubConnectionContext connection)
        => connection.User?.Claims!.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
}