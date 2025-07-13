using JF.OrdemServico.Domain.Common;
using JF.OrdemServico.Domain.Entities;
using JF.OrdemServico.Domain.ValueObjects;

namespace JF.OrdemServico.Domain.Repositories;

public interface IChamadoRepository : IRepositoryBase<Chamado>
{
    Task<IEnumerable<Chamado>> FiltrarPorStatusAsync(ChamadoStatus status);

    Task<IEnumerable<Chamado>> FiltrarPorPrioridadeAsync(ChamadoPrioridade prioridade);

    Task<IEnumerable<Chamado>> FiltrarPorClienteAsync(Guid clienteId);
}