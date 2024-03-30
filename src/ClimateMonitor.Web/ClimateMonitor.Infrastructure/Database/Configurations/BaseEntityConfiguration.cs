using ClimateMonitor.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ClimateMonitor.Infrastructure.Database.Configurations;

public class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.ToTable(GetTableName<T>());
        builder.Property(e => e.CreatedBy).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
    }

    private string GetTableName<TEntity>() => typeof(TEntity).Name.Replace("Entity", "s");
}