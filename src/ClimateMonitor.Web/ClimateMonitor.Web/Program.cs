using Azure.Monitor.OpenTelemetry.AspNetCore;
using ClimateMonitor.Application.Abstraction;
using ClimateMonitor.Infrastructure;
using ClimateMonitor.Web.Authorization;
using ClimateMonitor.Web.Hubs;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
var openTelemetryConnectionString = builder.Configuration.GetValue<string>("AzureMonitor:ConnectionString");
if (!string.IsNullOrEmpty(openTelemetryConnectionString))
{
    builder.Services.AddOpenTelemetry().UseAzureMonitor(options => options.ConnectionString = openTelemetryConnectionString);
}

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthenticationWithBearer();
builder.Services.AddAuthorization(options => options.AddPolicies());
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("ClimateMonitorDatabase")!);
builder.Services.AddScoped<IDeviceConnection, DeviceConnection>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseWebSockets();

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "ClientApp/build")),
    RequestPath = ""
});
app.MapHub<SensorConfigurationHub>("/configuration");

app.MapRazorPages();
app.MapControllers();
app.MapControllerRoute(
    name: "api",
    pattern: "/api/{controller=Home}/{id?}");

app.MapFallbackToPage("/settings/{*path:nonfile}", "/settings");

await app.InitializeDatabase();

app.Run();
