using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oid85.HomeBot.DataAccess.Entities.Base;
using Oid85.HomeBot.DataAccess.Schemas;

namespace Oid85.HomeBot.DataAccess.Configurations;

internal abstract class EntityConfigurationBase<TEntity> : IEntityTypeConfiguration<TEntity>, IHomeBotSchema 
    where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("gen_random_uuid()");
    }
}