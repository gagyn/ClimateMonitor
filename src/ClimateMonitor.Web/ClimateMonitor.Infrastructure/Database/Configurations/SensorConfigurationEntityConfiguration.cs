using ClimateMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClimateMonitor.Infrastructure.Database.Configurations;

public class SensorConfigurationEntityConfiguration : BaseEntityConfiguration<SensorConfigurationEntity>
{
    public override void Configure(EntityTypeBuilder<SensorConfigurationEntity> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.SensorId);
        
        builder.Property(x => x.Pin).IsRequired();
        builder.Property(x => x.SensorType).HasConversion<string>();
        builder.Property(x => x.IsActive).IsRequired();
    }
}