using ClimateMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClimateMonitor.Infrastructure.Database.Configurations;

public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.CreatedBy).IsRequired();
    }
}