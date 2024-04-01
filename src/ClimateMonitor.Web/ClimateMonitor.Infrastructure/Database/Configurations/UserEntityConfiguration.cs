using ClimateMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClimateMonitor.Infrastructure.Database.Configurations;
internal class UserEntityConfiguration : BaseEntityConfiguration<UserEntity>
{
    public override void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.BaseUser)
            .WithOne()
            .HasForeignKey<UserEntity>(x => x.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Devices)
            .WithOne()
            .HasForeignKey(x => x.UserOwnerId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
