using ClimateMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClimateMonitor.Infrastructure.Database.Configurations;

public class RecordEntityConfiguration : BaseEntityConfiguration<RecordEntity>
{
    public override void Configure(EntityTypeBuilder<RecordEntity> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.Id);
        builder.HasOne<DeviceEntity>()
            .WithMany()
            .HasForeignKey(x => x.DeviceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<SensorConfigurationEntity>()
            .WithMany()
            .HasForeignKey(x => x.SensorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(x => x.ReadAt).IsRequired();
        builder.Property(x => x.Temperature).HasPrecision(5, 2);
        builder.Property(x => x.Humidity).HasPrecision(3, 2);
    }
}