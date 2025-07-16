using JF.OrdemServico.Domain.Common;

namespace JF.OrdemServico.Domain.Entities;

public class Cliente : EntityBase
{
    public string Nome { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização
    public string Email { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização

    // Construtor usado pelo EF Core
    protected Cliente() { }

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
