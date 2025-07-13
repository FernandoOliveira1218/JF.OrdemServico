using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Domain.ValueObjects;

namespace JF.OrdemServico.Domain.Repositories;

public interface IChamadoRepository : IRepositoryBase<Chamado>
{
    Task<IEnumerable<Chamado>> ObterPorStatusAsync(ChamadoStatus status);

    Task<IEnumerable<Chamado>> ObterPorPrioridadeAsync(ChamadoPrioridade prioridade);

    Task<IEnumerable<Chamado>> ObterPorClienteAsync(Guid clienteId);
}