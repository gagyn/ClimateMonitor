using ClimateMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClimateMonitor.Infrastructure.Database.Configurations;

internal class DeviceUserEntityConfiguration : BaseEntityConfiguration<DeviceUserEntity>
{
    public override void Configure(EntityTypeBuilder<DeviceUserEntity> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.DeviceId);
        builder.HasOne(x => x.BaseUser);
    }
}
