using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Domain.ValueObjects;
using JF.OrdemServico.Infra.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ChamadoMap : EntityMapBase<Chamado>
{
    public override void Configure(EntityTypeBuilder<Chamado> builder)
    {
        base.Configure(builder);

        builder.ToTable("Chamados");

        builder.Property(c => c.Descricao)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.Prioridade)
            .IsRequired()
            .HasConversion(p => p.Value, v => ChamadoPrioridade.FromValue(v));

        builder.Property(c => c.Status)
            .IsRequired()
            .HasConversion(s => s.Value, v => ChamadoStatus.FromValue(v));

        builder.Property(c => c.DataConclusao);

        builder.HasOne(c => c.Cliente);
    }
}