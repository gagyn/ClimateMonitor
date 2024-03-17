using ClimateMonitor.Application.Authorization;
using ClimateMonitor.Application.Handlers;
using ClimateMonitor.Domain.Entities;
using ClimateMonitor.Domain.Repositories;
using ClimateMonitor.Infrastructure.Authorization;
using ClimateMonitor.Infrastructure.Database;
using ClimateMonitor.Infrastructure.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ClimateMonitor.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ClimateDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<IUserContext, UserContext>();
        services.AddSingleton(TimeProvider.System);
        services.AddMediatR(c =>
        {
            c.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            c.RegisterServicesFromAssembly(typeof(AddRecordCommand).Assembly);
        });
        services.AddRepositories();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDeviceConfigurationRepository, DeviceConfigurationRepository>();
        services.AddScoped<IRecordRepository, RecordRepository>();
        services.AddScoped<ISensorConfigurationRepository, SensorConfigurationRepository>();
        return services;
    }

    public static async Task InitializeDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ClimateDbContext>();
        dbContext.Database.Migrate();

        var roles = new[] { RoleEntity.User, RoleEntity.Device };
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(role.ToString()));
            }
        }
    }
}