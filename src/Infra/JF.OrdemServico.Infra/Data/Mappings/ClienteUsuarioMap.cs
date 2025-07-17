using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Infra.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JF.OrdemServico.Infra.Data.Mappings;

public class ClienteUsuarioConfiguration : EntityMapBase<ClienteUsuario>
{
    public override void Configure(EntityTypeBuilder<ClienteUsuario> builder)
    {
        base.Configure(builder);

        builder.ToTable("ClientesUsuarios");

        builder.HasOne(cu => cu.Cliente).WithMany(c => c.ClienteUsuarios).HasForeignKey(cu => cu.ClienteId);

        builder.HasOne(cu => cu.Usuario).WithMany(u => u.ClienteUsuarios).HasForeignKey(cu => cu.UsuarioId);
    }
}