using ClimateMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClimateMonitor.Infrastructure.Database.Configurations;

public class DeviceEntityConfiguration : BaseEntityConfiguration<DeviceEntity>
{
    public override void Configure(EntityTypeBuilder<DeviceEntity> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.DeviceId);
        builder.Property(x => x.Name).IsRequired();

        builder.HasMany(x => x.SensorConfigurations)
            .WithOne()
            .HasForeignKey(x => x.DeviceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}