using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Infra.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ClienteMap : EntityMapBase<Cliente>
{
    public override void Configure(EntityTypeBuilder<Cliente> builder)
    {
        base.Configure(builder);

        builder.ToTable("Clientes");

        builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(100);
        builder.Property(c => c.RazaoSocial).IsRequired().HasMaxLength(150);
        builder.Property(c => c.Cnpj).IsRequired().HasMaxLength(20);
    }
}