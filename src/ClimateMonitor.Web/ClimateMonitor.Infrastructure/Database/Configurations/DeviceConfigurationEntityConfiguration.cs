using ClimateMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClimateMonitor.Infrastructure.Database.Configurations;

public class DeviceConfigurationEntityConfiguration : BaseEntityConfiguration<DeviceConfigurationEntity>
{
    public override void Configure(EntityTypeBuilder<DeviceConfigurationEntity> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.DeviceId);
        builder.Property(x => x.Name).IsRequired();
    }
}