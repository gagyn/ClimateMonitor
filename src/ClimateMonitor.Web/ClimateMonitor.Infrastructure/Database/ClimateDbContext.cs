using ClimateMonitor.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ClimateMonitor.Infrastructure.Database;
public class ClimateDbContext(DbContextOptions<ClimateDbContext> options) : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<RecordEntity> Records { get; set; }
    public DbSet<DeviceConfigurationEntity> DeviceConfigurations { get; set; }
    public DbSet<SensorConfigurationEntity> SensorConfigurations { get; set; }

    public const string DefaultSchema = "climateMonitor";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(DefaultSchema);
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
