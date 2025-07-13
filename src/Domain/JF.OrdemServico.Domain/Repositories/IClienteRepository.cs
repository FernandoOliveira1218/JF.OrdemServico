using JF.OrdemServico.Domain.Common;
using JF.OrdemServico.Domain.Entities;

namespace JF.OrdemServico.Domain.Repositories;

public interface IClienteRepository : IRepositoryBase<Cliente>
{
    Task<Cliente?> ObterPorEmailAsync(string email);
}