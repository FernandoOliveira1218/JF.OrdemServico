using JF.OrdemServico.Domain.Entities;

namespace JF.OrdemServico.Domain.Interfaces.Repositories;

public interface IUsuarioRepository : IRepositoryBase<Usuario>
{
    Task<Usuario?> GetByLoginAsync(string login);
}
