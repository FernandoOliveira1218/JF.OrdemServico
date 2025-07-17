using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Infra.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JF.OrdemServico.Infra.Data.Mappings;

public class UsuarioMap : EntityMapBase<Usuario>
{
    public override void Configure(EntityTypeBuilder<Usuario> builder)
    {
        base.Configure(builder);

        builder.ToTable("Usuarios");

        builder.Property(u => u.Nome).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Login).IsRequired().HasMaxLength(50);
        builder.Property(u => u.SenhaHash).IsRequired().HasMaxLength(255);
    }
}