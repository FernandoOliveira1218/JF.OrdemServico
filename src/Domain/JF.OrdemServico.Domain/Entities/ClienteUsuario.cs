using JF.OrdemServico.Domain.Common;

namespace JF.OrdemServico.Domain.Entities;

public class ClienteUsuario : EntityBase
{
    public Guid ClienteId { get; private set; }
    public Cliente Cliente { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização

    public Guid UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; } = null!; // Usando null-forgiving operator para indicar que não será nulo após a inicialização

    protected ClienteUsuario() { }

    public ClienteUsuario(Guid clienteId, Guid usuarioId)
    {
        ClienteId = clienteId;
        UsuarioId = usuarioId;
    }
}
