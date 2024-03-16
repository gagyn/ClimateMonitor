using ClimateMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ClimateMonitor.Infrastructure.Database;
public class ClimateDbContext(DbContextOptions<ClimateDbContext> options) : IdentityDbContext<UserEntity>(options)
{
    public DbSet<RecordEntity> Records { get; set; }
    public DbSet<DeviceConfigurationEntity> DeviceConfigurations { get; set; }
    public DbSet<SensorConfigurationEntity> SensorConfigurations { get; set; }

    public const string DefaultSchema = "climateMonitor";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(message => Debug.WriteLine(message));
        optionsBuilder.UseSqlServer();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchema);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
