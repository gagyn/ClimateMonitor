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
        serviceCollection.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = BearerOrCookieSchema;
            options.DefaultChallengeScheme = BearerOrCookieSchema;
        })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LogoutPath = "/login";
                options.Events = new()
                {
                    OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        return Task.CompletedTask;
                    }
                };
            })
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