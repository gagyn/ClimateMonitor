using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Infrastructure;
using ClimateMonitor.Web.API.Authorization;
using ClimateMonitor.Web.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//todo: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/websockets?view=aspnetcore-8.0

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthenticationWithBearer();
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
app.MapHub<SensorConfigurationHub>("/configuration");
app.MapIdentityApi<UserEntity>();

await app.InitializeDatabase();

app.Run();
