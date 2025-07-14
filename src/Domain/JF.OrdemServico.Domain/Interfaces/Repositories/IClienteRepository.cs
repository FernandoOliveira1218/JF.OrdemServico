using JF.OrdemServico.Domain.Entities;

namespace JF.OrdemServico.Domain.Interfaces.Repositories;

public interface IClienteRepository : IRepositoryBase<Cliente>
{
    Task<Cliente?> ObterPorEmailAsync(string email);
}