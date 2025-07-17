using JF.OrdemServico.Domain.Common;

namespace JF.OrdemServico.Domain.Entities;

public class Usuario : EntityBase
{
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Login { get; private set; } = string.Empty;
    public string SenhaHash { get; private set; } = string.Empty;

    public ICollection<ClienteUsuario> ClienteUsuarios { get; private set; } = [];

    protected Usuario() { }

    public Usuario(string nome, string email, string login, string senhaHash)
    {
        Nome = nome;
        Email = email;
        Login = login;
        SenhaHash = senhaHash;
    }
}