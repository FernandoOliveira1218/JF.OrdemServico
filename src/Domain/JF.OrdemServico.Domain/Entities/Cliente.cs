using JF.OrdemServico.Domain.Common;

namespace JF.OrdemServico.Domain.Entities;

public class Cliente : EntityBase
{
    public string Nome { get; private set; }
    public string Email { get; private set; }

    public Cliente(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }

    public void Atualizar(string nome, string email)
    {
        Nome = nome;
        Email = email;
        AtualizarDataAlteracao();
    }
}
