using Azure.Monitor.OpenTelemetry.AspNetCore;
using ClimateMonitor.Infrastructure;
using ClimateMonitor.Web.API.Authorization;
using ClimateMonitor.Web.API.Hubs;

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

app.MapControllers();
app.UseStaticFiles();
app.MapHub<SensorConfigurationHub>("/configuration");

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllers();
});

await app.InitializeDatabase();

app.Run();
