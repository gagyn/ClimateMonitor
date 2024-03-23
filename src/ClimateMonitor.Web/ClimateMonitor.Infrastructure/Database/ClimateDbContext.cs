using ClimateMonitor.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ClimateMonitor.Infrastructure.Database;
public class ClimateDbContext(DbContextOptions<ClimateDbContext> options) : IdentityDbContext<BaseUserEntity, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<RecordEntity> Records { get; set; }
    public DbSet<DeviceEntity> Devices { get; set; }
    public DbSet<SensorConfigurationEntity> SensorConfigurations { get; set; }
    public new DbSet<UserEntity> Users { get; set; }
    public DbSet<DeviceUserEntity> DeviceUsers { get; set; }
    public DbSet<BaseUserEntity> BaseUsers => base.Users;

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
