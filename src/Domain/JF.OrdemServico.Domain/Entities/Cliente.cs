using JF.OrdemServico.Domain.Common;

namespace JF.OrdemServico.Domain.Entities;

public class Cliente : EntityBase
{
    public string Nome { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização
    public string Email { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização
    public string RazaoSocial { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização
    public string Cnpj { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização

    public ICollection<ClienteUsuario> ClienteUsuarios { get; private set; } = [];

    // Construtor usado pelo EF Core
    protected Cliente() { }

    public Cliente(string nome, string email, string razaoSocial, string cnpj)
    {
        Nome = nome;
        Email = email;
        RazaoSocial = razaoSocial;
        Cnpj = cnpj;
    }

    public void Atualizar(string nome, string email, string razaoSocial, string cnpj)
    {
        Nome = nome;
        Email = email;
        RazaoSocial = razaoSocial;
        Cnpj = cnpj;

        AtualizarDataAlteracao();
    }
}
