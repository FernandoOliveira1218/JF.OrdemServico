using JF.OrdemServico.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace JF.OrdemServico.Infra.Data.Context;

public static class OrdemServicoSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var clienteId = new Guid("11111111-1111-1111-1111-111111111111");
        var usuarioId = new Guid("22222222-2222-2222-2222-222222222222");
        var clienteUsuarioId = new Guid("33333333-3333-3333-3333-333333333333");
        var dataCriacao = new DateTime(2025, 7, 16, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Cliente>().HasData(new
        {
            Id = clienteId,
            Nome = "Cliente Padrão",
            Email = "clientepadrao@cliente.com",
            RazaoSocial = "Cliente Padrão LTDA",
            Cnpj = "12345678000100",
            DataCriacao = dataCriacao,
            DataAlteracao = (DateTime?)null
        });

        modelBuilder.Entity<Usuario>().HasData(new
        {
            Id = usuarioId,
            Nome = "Administrador",
            Email = "admin@admin.com",
            Login = "admin",
            SenhaHash = "$2a$11$4LC85MBAVfGLbtymgz8VcetHq7NMg/wUQppvHomk6whsCJXG3.ony",
            DataCriacao = dataCriacao,
            DataAlteracao = (DateTime?)null
        });

        modelBuilder.Entity<ClienteUsuario>().HasData(new
        {
            Id = clienteUsuarioId,
            ClienteId = clienteId,
            UsuarioId = usuarioId,
            DataCriacao = dataCriacao,
            DataAlteracao = (DateTime?)null
        });
    }

}