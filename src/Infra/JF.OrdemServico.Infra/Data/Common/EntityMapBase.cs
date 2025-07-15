using JF.OrdemServico.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JF.OrdemServico.Infra.Data.Common;

public abstract class EntityMapBase<T> : IEntityTypeConfiguration<T> where T : EntityBase
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.DataCriacao)
               .IsRequired();

        builder.Property(e => e.DataAlteracao)
               .IsRequired(false);
    }
}